using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class LongValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw()
        => Assert.Equal(9L, new LongValueObject(9L).Value);

    [Fact]
    public void From_KeepsValue()
        => Assert.Equal(9L, LongValueObject.From(9L).Value);

    // The validator has no rules, so Create never throws.
    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal(long.MaxValue, LongValueObject.Create(long.MaxValue).Value);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        long value = LongValueObject.Create(9L);
        Assert.Equal(9L, value);
    }
}
