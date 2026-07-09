using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class StringValueObjectTests
{
    [Fact]
    public void From_TrimsWhitespace()
        => Assert.Equal("hi", StringValueObject.From("  hi  ").Value);

    [Fact]
    public void Create_TrimsWhitespace()
        => Assert.Equal("hi", StringValueObject.Create("  hi  ").Value);

    // The base validator has no rules, so Create never throws — not even on empty input.
    [Fact]
    public void Create_BaseHasNoRules_DoesNotThrowOnEmpty()
        => Assert.Equal("", StringValueObject.Create("").Value);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        string value = StringValueObject.Create("hi");
        Assert.Equal("hi", value);
    }

    [Fact]
    public void Equality_SameTypeSameValue_AreEqual()
        => Assert.Equal(EmailValueObject.Create("a@b.com"), EmailValueObject.Create("a@b.com"));

    [Fact]
    public void Equality_DifferentVoTypesSameValue_AreNotEqual()
        // Record equality includes the runtime type, so two different VO types with the same value differ.
        => Assert.NotEqual<StringValueObject>(EmailValueObject.From("abc"), UrlValueObject.From("abc"));
}
