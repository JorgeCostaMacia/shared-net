using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class StringValueObjectTests
{
    [Fact]
    public void Create_FromString_TrimsWhitespace()
        => Assert.Equal("hi", StringValueObject.Create("  hi  ").Value);

    // The non-string overloads funnel through the root Convert(string) (which trims).
    [Fact]
    public void Create_FromOtherTypes_FunnelsThroughRoot()
    {
        Assert.Equal("42", StringValueObject.Create(42).Value);
        Assert.Equal("True", StringValueObject.Create(true).Value);
    }

    [Fact]
    public void Create_FromGuid_UsesGuidString()
    {
        Guid id = Guid.NewGuid();
        Assert.Equal(id.ToString(), StringValueObject.Create(id).Value);
    }

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
        => Assert.NotEqual<StringValueObject>(EmailValueObject.Create("abc"), UrlValueObject.Create("abc"));
}
