using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DoubleValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw()
        => Assert.Equal(2.5d, new DoubleValueObject(2.5d).Value);

    [Fact]
    public void From_KeepsValue()
        => Assert.Equal(2.5d, DoubleValueObject.From(2.5d).Value);

    // The validator has no rules, so Create never throws.
    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal(2.5d, DoubleValueObject.Create(2.5d).Value);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        double value = DoubleValueObject.Create(2.5d);
        Assert.Equal(2.5d, value);
    }
}
