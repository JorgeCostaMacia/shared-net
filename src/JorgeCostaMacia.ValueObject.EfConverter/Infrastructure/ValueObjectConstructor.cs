using System.Linq.Expressions;
using System.Reflection;

namespace JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

/// <summary>
/// Builds the "from provider" expression the family converters share: rehydrates a value object by
/// calling its constructor (found by reflection once, compiled into the expression), so reads go through
/// the infrastructure constructor — not the <c>Create</c> factory — and skip re-validating persisted data.
/// The constructor may be non-public (e.g. a <c>protected</c> one).
/// </summary>
internal static class ValueObjectConstructor
{
    /// <summary>Returns <c>value =&gt; new TValueObject(value)</c> for the single-<typeparamref name="TValue"/> constructor.</summary>
    /// <typeparam name="TValueObject">The value object type to construct.</typeparam>
    /// <typeparam name="TValue">The underlying primitive the constructor takes.</typeparam>
    public static Expression<Func<TValue, TValueObject>> From<TValueObject, TValue>()
    {
        ConstructorInfo constructor = typeof(TValueObject).GetConstructor(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, [typeof(TValue)], null)
            ?? throw new InvalidOperationException($"'{typeof(TValueObject).Name}' needs a constructor taking a single '{typeof(TValue).Name}' to be rehydrated.");

        ParameterExpression value = Expression.Parameter(typeof(TValue), "value");

        return Expression.Lambda<Func<TValue, TValueObject>>(Expression.New(constructor, value), value);
    }
}
