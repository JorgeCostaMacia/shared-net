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
}
