using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class IntValueObjectTests
{
    [Fact]
    public void Create_FromString_ParsesNumber()
        => Assert.Equal(42, IntValueObject.Create("42").Value);

    [Fact]
    public void Create_Conversions()
    {
        Assert.Equal(7, IntValueObject.Create(7).Value);
        Assert.Equal(7, IntValueObject.Create(7L).Value);
        Assert.Equal(5, IntValueObject.Create(5.0f).Value);
        Assert.Equal(5, IntValueObject.Create(5.0).Value);
        Assert.Equal(5, IntValueObject.Create(5m).Value);
        Assert.Equal(1, IntValueObject.Create(true).Value);
        Assert.Equal(0, IntValueObject.Create(false).Value);
    }

    [Fact]
    public void Create_FromString_LargeValues_KeepFullPrecision()
    {
        // Regression: the old float-based parse lost precision above 2^24 and overflowed near int.MaxValue.
        Assert.Equal(int.MaxValue, IntValueObject.Create("2147483647").Value);
        Assert.Equal(16777217, IntValueObject.Create("16777217").Value);
    }

    [Fact]
    public void Create_FromFractional_TruncatesTowardZero()
    {
        Assert.Equal(5, IntValueObject.Create(5.9).Value);      // not rounded up
        Assert.Equal(2, IntValueObject.Create(2.5).Value);      // not banker's-rounded to 2/4
        Assert.Equal(3, IntValueObject.Create(3.5).Value);      // truncated, not rounded to 4
        Assert.Equal(-5, IntValueObject.Create(-5.9).Value);    // toward zero
        Assert.Equal(5, IntValueObject.Create("5.9").Value);
        Assert.Equal(5, IntValueObject.Create(5.9m).Value);
    }

    [Fact]
    public void Create_FromString_OutOfRange_Throws()
        => Assert.Throws<OverflowException>(() => IntValueObject.Create("2147483648"));

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        int value = IntValueObject.Create(7);
        Assert.Equal(7, value);
    }
}
