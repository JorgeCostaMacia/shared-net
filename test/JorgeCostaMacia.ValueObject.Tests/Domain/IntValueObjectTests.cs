using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class IntValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw()
        => Assert.Equal(7, new IntValueObject(7).Value);

    [Fact]
    public void From_KeepsValue()
        => Assert.Equal(7, IntValueObject.From(7).Value);

    // The validator has no rules, so Create never throws.
    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal(7, IntValueObject.Create(7).Value);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        int value = IntValueObject.Create(7);
        Assert.Equal(7, value);
    }
}
