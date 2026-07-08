using System.Linq.Expressions;
using System.Reflection;

namespace JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

/// <summary>
/// Builds the "from provider" expression the family converters share: rehydrates a value object by
/// calling its constructor, so reads go through the infrastructure constructor — not the <c>Create</c>
/// factory — and skip re-validating persisted data. The constructor may be non-public (e.g. a
/// <c>protected</c> one).
/// </summary>
/// <remarks>
/// The natural expression, <c>value =&gt; new TValueObject(value)</c>, does not compile: C# only allows a
/// <b>parameterless</b> constructor constraint (<c>where T : new()</c>), never one taking an argument — so
/// a parameterized constructor cannot be called on an open generic type. Reflection builds that same
/// expression by hand. It runs <b>once</b>, when the converter is created (EF caches converters with the
/// model); the compiled delegate then invokes the constructor directly on every read, as fast as a
/// hand-written <c>new</c>.
/// </remarks>
internal static class ValueObjectConstructor
{
    /// <summary>Returns <c>value =&gt; new TValueObject(value)</c> for the single-<typeparamref name="TValue"/> constructor.</summary>
    /// <typeparam name="TValueObject">The value object type to construct.</typeparam>
    /// <typeparam name="TValue">The underlying primitive the constructor takes.</typeparam>
    /// <returns>An expression that builds a <typeparamref name="TValueObject"/> from a single <typeparamref name="TValue"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when <typeparamref name="TValueObject"/> has no constructor taking a single <typeparamref name="TValue"/>.</exception>
    public static Expression<Func<TValue, TValueObject>> From<TValueObject, TValue>()
    {
        ConstructorInfo constructor = typeof(TValueObject).GetConstructor(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, [typeof(TValue)], null)
            ?? throw new InvalidOperationException($"'{typeof(TValueObject).Name}' needs a constructor taking a single '{typeof(TValue).Name}' to be rehydrated.");

        ParameterExpression value = Expression.Parameter(typeof(TValue), "value");

        return Expression.Lambda<Func<TValue, TValueObject>>(Expression.New(constructor, value), value);
    }
}
