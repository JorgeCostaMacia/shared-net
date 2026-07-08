using System.Globalization;
using System.Linq.Expressions;

namespace JorgeCostaMacia.ExpressionConverter.Domain;

/// <summary>
/// Provides utilities to convert between LINQ predicates and dictionaries of key-value pairs.
/// </summary>
/// <remarks>
/// The supported predicate shape is a flat conjunction of equality checks —
/// <c>x =&gt; x.Prop1 == value1 &amp;&amp; x.Prop2 == value2 &amp;&amp; ...</c> — where the property is on the
/// left of each <c>==</c> and the right side is any expression that evaluates to a value
/// (a literal, a captured variable, a method call, ...). It does NOT support other operators
/// (<c>!=</c>, <c>&lt;</c>, <c>&gt;</c>, <c>||</c>), nested members (<c>x.Child.Name</c>),
/// right-associative grouping (<c>a &amp;&amp; (b &amp;&amp; c)</c>), or repeated properties.
/// </remarks>
public static class ExpressionConverter
{
    /// <summary>
    /// Converts a flat conjunction of equality checks (e.g. <c>x =&gt; x.Prop1 == value1 &amp;&amp; x.Prop2 == value2</c>)
    /// into a dictionary mapping each property name to its value rendered as an invariant-culture string.
    /// </summary>
    /// <typeparam name="T">The type of the object in the expression.</typeparam>
    /// <param name="expression">
    /// A predicate composed of equality checks combined with AND, with the property on the left of each
    /// <c>==</c>. The right side may be any expression that evaluates to a value (constant, captured variable,
    /// computed) as long as it does not reference the lambda parameter. An enum is rendered by its name, a
    /// <see cref="DateTime"/> in round-trip ("O") format, and a <c>null</c> value as an empty string.
    /// </param>
    /// <returns>A dictionary mapping property names to their string values.</returns>
    /// <exception cref="InvalidCastException">
    /// Thrown when <paramref name="expression"/> does not match the expected shape — e.g. it uses an operator
    /// other than <c>==</c>/<c>&amp;&amp;</c>, the property is not on the left, or a member is nested.
    /// </exception>
    /// <exception cref="ArgumentException">Thrown when the same property appears more than once.</exception>
    public static Dictionary<string, string> Convert<T>(Expression<Func<T, bool>> expression)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        BinaryExpression body = (BinaryExpression)expression.Body;

        while (body.NodeType != ExpressionType.Equal)
        {
            ParseEquals((BinaryExpression)body.Right);
            body = (BinaryExpression)body.Left;
        }

        ParseEquals(body);

        void ParseEquals(BinaryExpression e)
        {
            MemberExpression key = (MemberExpression)Unwrap(e.Left);
            object? value = Expression.Lambda(Unwrap(e.Right)).Compile().DynamicInvoke();

            result.Add(key.Member.Name, Format(value, key.Type));
        }

        return result;
    }

    /// <summary>
    /// Converts a dictionary of property-value pairs back into a predicate whose body is a conjunction of
    /// equality checks. An empty dictionary yields an always-true predicate (<c>x =&gt; true</c>).
    /// </summary>
    /// <typeparam name="T">The type of the object for which the expression is built.</typeparam>
    /// <param name="dictionary">
    /// A dictionary mapping property names to their string values, as produced by
    /// <see cref="Convert{T}(Expression{Func{T, bool}})"/>. An empty string for a nullable property is
    /// interpreted as <c>null</c>.
    /// </param>
    /// <returns>A predicate expression equivalent to checking all dictionary entries for equality.</returns>
    /// <remarks>
    /// Values are converted to each property's type using, in order: <see cref="Nullable{T}"/> unwrapping
    /// (with empty string mapped to <c>null</c>), <see cref="Guid.Parse(string)"/> for <see cref="Guid"/>,
    /// <see cref="Enum.Parse(Type, string, bool)"/> for enums, round-trip parsing for <see cref="DateTime"/>,
    /// and invariant-culture <see cref="System.Convert.ChangeType(object, Type, IFormatProvider)"/> for any
    /// other <see cref="IConvertible"/> type.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown when a dictionary key does not correspond to a public property of <typeparamref name="T"/>.
    /// </exception>
    /// <exception cref="InvalidCastException">
    /// Thrown when a property's type is neither <see cref="Guid"/>, an enum, <see cref="DateTime"/>, nor a
    /// type that implements <see cref="IConvertible"/>.
    /// </exception>
    /// <exception cref="FormatException">
    /// Thrown when a value's string representation is not valid for the target property's type.
    /// </exception>
    public static Expression<Func<T, bool>> ConvertBack<T>(Dictionary<string, string> dictionary)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");

        List<Expression> expressions = new List<Expression>();

        foreach (KeyValuePair<string, string> d in dictionary)
        {
            MemberExpression property = Expression.Property(parameter, d.Key);

            object? convertedValue = ConvertValue(d.Value, property.Type);
            ConstantExpression constant = Expression.Constant(convertedValue, property.Type);

            BinaryExpression equality = Expression.Equal(property, constant);
            expressions.Add(equality);
        }

        Expression body = expressions.Count switch
        {
            0 => Expression.Constant(true),
            1 => expressions[0],
            _ => expressions.Aggregate(Expression.AndAlso)
        };

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>Strips a compiler-inserted <c>Convert</c>/<c>ConvertChecked</c> node (e.g. around enum comparisons).</summary>
    private static Expression Unwrap(Expression expression)
        => expression is UnaryExpression { NodeType: ExpressionType.Convert or ExpressionType.ConvertChecked } unary
            ? unary.Operand
            : expression;

    /// <summary>Renders a value as an invariant, round-trippable string (enum by name, DateTime as "O", null as empty).</summary>
    private static string Format(object? value, Type memberType)
    {
        if (value is null)
        {
            return "";
        }

        Type type = Nullable.GetUnderlyingType(memberType) ?? memberType;

        if (type.IsEnum)
        {
            return Enum.GetName(type, value) ?? System.Convert.ToString(value, CultureInfo.InvariantCulture) ?? "";
        }

        if (type == typeof(DateTime))
        {
            return ((DateTime)value).ToString("O", CultureInfo.InvariantCulture);
        }

        return System.Convert.ToString(value, CultureInfo.InvariantCulture) ?? "";
    }

    /// <summary>
    /// Converts a string value into an instance of <paramref name="targetType"/>, with explicit support for
    /// <see cref="Nullable{T}"/>, <see cref="Guid"/>, enums, and <see cref="DateTime"/> (which do not round-trip
    /// through <see cref="System.Convert.ChangeType(object, Type, IFormatProvider)"/> alone).
    /// </summary>
    /// <param name="value">The string representation of the value. An empty string represents <c>null</c> for nullable target types.</param>
    /// <param name="targetType">The type to convert <paramref name="value"/> into.</param>
    /// <returns>The converted value, or <c>null</c> if <paramref name="value"/> is empty and <paramref name="targetType"/> is nullable.</returns>
    private static object? ConvertValue(string value, Type targetType)
    {
        Type? underlyingType = Nullable.GetUnderlyingType(targetType);

        if (underlyingType is not null)
        {
            if (value.Length == 0)
            {
                return null;
            }

            targetType = underlyingType;
        }

        if (targetType == typeof(Guid))
        {
            return Guid.Parse(value);
        }

        if (targetType.IsEnum)
        {
            return Enum.Parse(targetType, value, ignoreCase: true);
        }

        if (targetType == typeof(DateTime))
        {
            return DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
        }

        return System.Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
    }
}
