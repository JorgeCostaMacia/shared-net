using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class BoolValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw()
        => Assert.True(new BoolValueObject(true).Value);

    [Fact]
    public void From_KeepsValue()
    {
        Assert.True(BoolValueObject.From(true).Value);
        Assert.False(BoolValueObject.From(false).Value);
    }

    // The validator has no rules, so Create never throws.
    [Fact]
    public void Create_KeepsValue()
    {
        Assert.True(BoolValueObject.Create(true).Value);
        Assert.False(BoolValueObject.Create(false).Value);
    }

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        bool value = BoolValueObject.Create(true);
        Assert.True(value);
    }
}
