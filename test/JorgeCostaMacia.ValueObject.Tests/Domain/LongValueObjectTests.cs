using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class LongValueObjectTests
{
    [Fact]
    public void Create_FromInt_DoesNotRecurse()
        => Assert.Equal(5L, LongValueObject.Create(5).Value);

    [Fact]
    public void Create_Conversions()
    {
        Assert.Equal(42L, LongValueObject.Create("42").Value);
        Assert.Equal(9L, LongValueObject.Create(9L).Value);
        Assert.Equal(5L, LongValueObject.Create(5.0d).Value);
        Assert.Equal(5L, LongValueObject.Create(5m).Value);
        Assert.Equal(1L, LongValueObject.Create(true).Value);
    }

    [Fact]
    public void Create_FromString_LargeValues_KeepFullPrecision()
    {
        // Regression: the old double-based parse lost precision above 2^53 and overflowed near long.MaxValue.
        Assert.Equal(long.MaxValue, LongValueObject.Create("9223372036854775807").Value);
        Assert.Equal(9007199254740993L, LongValueObject.Create("9007199254740993").Value);
    }

    [Fact]
    public void Create_FromFractional_TruncatesTowardZero()
    {
        Assert.Equal(5L, LongValueObject.Create(5.9).Value);
        Assert.Equal(3L, LongValueObject.Create(3.5).Value);    // truncated, not rounded
        Assert.Equal(-5L, LongValueObject.Create(-5.9).Value);
        Assert.Equal(5L, LongValueObject.Create("5.9").Value);
        Assert.Equal(5L, LongValueObject.Create(5.9m).Value);
    }

    [Fact]
    public void Create_FromString_OutOfRange_Throws()
        => Assert.Throws<OverflowException>(() => LongValueObject.Create("9223372036854775808"));

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        long value = LongValueObject.Create(9L);
        Assert.Equal(9L, value);
    }
}
