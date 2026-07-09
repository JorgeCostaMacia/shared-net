using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DecimalValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw()
        => Assert.Equal(1.5m, new DecimalValueObject(1.5m).Value);

    [Fact]
    public void From_KeepsValue()
        => Assert.Equal(1.5m, DecimalValueObject.From(1.5m).Value);

    // The validator has no rules, so Create never throws.
    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal(1.5m, DecimalValueObject.Create(1.5m).Value);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        decimal value = DecimalValueObject.Create(1.5m);
        Assert.Equal(1.5m, value);
    }
}
