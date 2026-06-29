using System.Linq.Expressions;

namespace JorgeCostaMacia.ExpressionConverter.Domain;

/// <summary>
/// Provides utilities to convert between LINQ expressions and dictionaries of key-value pairs.
/// </summary>
public static class ExpressionConverter
{
    /// <summary>
    /// Converts a simple binary expression (e.g., x => x.Prop1 == value1 &amp;&amp; x.Prop2 == value2) into a dictionary.
    /// Each entry maps a property name to its constant value as a string.
    /// </summary>
    /// <typeparam name="T">The type of the object in the expression.</typeparam>
    /// <param name="expression">
    /// A binary expression composed entirely of equality checks combined with AND operations,
    /// where the property must always be on the left-hand side of each equality and the value
    /// must always be a constant on the right-hand side. A <c>null</c> constant value is
    /// represented as an empty string.
    /// </param>
    /// <returns>A dictionary mapping property names to their string values.</returns>
    /// <exception cref="InvalidCastException">
    /// Thrown when <paramref name="expression"/> does not match the expected shape — for example,
    /// if it contains operators other than <c>==</c> and <c>&amp;&amp;</c>, or if a property and a
    /// constant are not in the expected left/right order.
    /// </exception>
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
            MemberExpression key = (MemberExpression)e.Left;
            ConstantExpression value = (ConstantExpression)e.Right;

            result.Add(key.Member.Name, value.Value?.ToString() ?? "");
        }

        return result;
    }

    /// <summary>
    /// Converts a dictionary of property-value pairs back into a lambda expression whose body
    /// represents equality checks combined with AND operations.
    /// </summary>
    /// <typeparam name="T">The type of the object for which the expression is built.</typeparam>
    /// <param name="dictionary">
    /// A dictionary mapping property names to their string values, as produced by
    /// <see cref="Convert{T}(Expression{Func{T, bool}})"/>. An empty string for a nullable
    /// property is interpreted as <c>null</c>.
    /// </param>
    /// <returns>A predicate expression equivalent to checking all dictionary entries for equality.</returns>
    /// <remarks>
    /// Values are converted to each property's type using, in order: <see cref="Nullable{T}"/>
    /// unwrapping (with empty string mapped to <c>null</c>), <see cref="Guid.Parse(string)"/> for
    /// <see cref="Guid"/> properties, <see cref="Enum.Parse(Type, string, bool)"/> for enum
    /// properties, and <see cref="System.Convert.ChangeType(object, Type)"/> as a fallback for
    /// any other <see cref="IConvertible"/> type (numeric types, <see cref="string"/>,
    /// <see cref="bool"/>, <see cref="DateTime"/>, <see cref="decimal"/>...).
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown when a dictionary key does not correspond to a public property of <typeparamref name="T"/>.
    /// </exception>
    /// <exception cref="InvalidCastException">
    /// Thrown when a property's type is neither <see cref="Guid"/>, an enum, nor a type that
    /// implements <see cref="IConvertible"/>.
    /// </exception>
    /// <exception cref="FormatException">
    /// Thrown when a value's string representation is not valid for the target property's type
    /// (e.g., an invalid <see cref="Guid"/> or enum literal).
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

        Expression? body = expressions.Count > 1 ? expressions.Aggregate(Expression.AndAlso) : expressions.First();

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// Converts a string value into an instance of <paramref name="targetType"/>, with explicit
    /// support for <see cref="Nullable{T}"/>, <see cref="Guid"/>, and enum types, since these do
    /// not implement <see cref="IConvertible"/> and are therefore unsupported by
    /// <see cref="System.Convert.ChangeType(object, Type)"/> alone.
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

        return System.Convert.ChangeType(value, targetType);
    }
}
