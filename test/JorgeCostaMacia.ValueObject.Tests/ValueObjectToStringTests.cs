using System.Reflection;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

/// <summary>
/// Contract test over the whole library: no value object may print like a record.
/// </summary>
/// <remarks>
/// A record synthesizes its own <c>ToString()</c> override unless it declares one, and the synthesized
/// one prints <c>TypeName { Value = … }</c>. That silently shadowed the bases' hand-written override in
/// every derived value object, so ten of them leaked their type name into log lines and interpolated
/// strings. The fix is <c>sealed</c> on the roots' override, which stops the compiler from generating
/// one further down. The two are told apart by <see cref="MethodBase.IsFinal"/>: the hand-written
/// sealed override is final, the compiler's is not — so this catches a root that drops the keyword,
/// and every consumer's value object inherits the guarantee.
/// </remarks>
public class ValueObjectToStringTests
{
    private static readonly Type[] _valueObjects = typeof(IValueObject).Assembly.GetTypes()
        .Where(type => type is { IsClass: true, IsAbstract: false } && typeof(IValueObject).IsAssignableFrom(type))
        .OrderBy(type => type.Name, StringComparer.Ordinal)
        .ToArray();

    [Fact]
    public void EveryValueObject_ResolvesToTheSealedToStringAndNotTheRecordOne()
    {
        Assert.NotEmpty(_valueObjects);

        List<string> violations = new List<string>();
        foreach (Type valueObject in _valueObjects)
        {
            MethodInfo toString = valueObject.GetMethod("ToString", Type.EmptyTypes)!;
            if (!toString.IsFinal)
            {
                violations.Add($"{valueObject.Name}: ToString() comes from {toString.DeclaringType?.Name} and is not sealed — a record-synthesized override is shadowing the root's");
            }
        }

        Assert.True(violations.Count == 0, "Record-synthesized ToString() found on:" + Environment.NewLine + string.Join(Environment.NewLine, violations));
    }

    // Spot-check on the two levels the reflection test generalizes: a root and something derived from it.
    [Fact]
    public void ToString_OnARootAndOnADerived_BothShowTheBareValue()
    {
        Assert.Equal("42", IntValueObject.From(42).ToString());
        Assert.Equal("7", PageSizeValueObject.From(7).ToString());
        Assert.Equal("user@host.com", EmailValueObject.From("user@host.com").ToString());
    }
}
