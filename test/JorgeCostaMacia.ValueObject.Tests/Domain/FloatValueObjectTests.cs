using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class FloatValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw()
        => Assert.Equal(2.5f, new FloatValueObject(2.5f).Value);

    [Fact]
    public void From_KeepsValue()
        => Assert.Equal(2.5f, FloatValueObject.From(2.5f).Value);

    // The validator has no rules, so Create never throws.
    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal(2.5f, FloatValueObject.Create(2.5f).Value);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        float value = FloatValueObject.Create(2.5f);
        Assert.Equal(2.5f, value);
    }
}
