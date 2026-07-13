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

    // The protected Convert family is the toolbox for derived VOs in consuming contexts —
    // exercised through a derived test type, like a real derived VO would.
    [Fact]
    public void Convert_FromString_ParsesNumber()
        => Assert.Equal(42L, TestLong.Convert("42"));

    [Fact]
    public void Convert_FromString_LargeValues_KeepFullPrecision()
    {
        // Regression: a double-based parse loses precision above 2^53 and overflows near long.MaxValue.
        Assert.Equal(long.MaxValue, TestLong.Convert("9223372036854775807"));
        Assert.Equal(9007199254740993L, TestLong.Convert("9007199254740993"));
    }

    [Fact]
    public void Convert_FromString_OutOfRange_Throws()
        => Assert.Throws<OverflowException>(() => TestLong.Convert("9223372036854775808"));

    [Fact]
    public void Convert_FromFractional_TruncatesTowardZero()
    {
        Assert.Equal(5L, TestLong.Convert(5.9));
        Assert.Equal(3L, TestLong.Convert(3.5));    // truncated, not rounded
        Assert.Equal(-5L, TestLong.Convert(-5.9));
        Assert.Equal(5L, TestLong.Convert("5.9"));
        Assert.Equal(5L, TestLong.Convert(5.9m));
    }

    [Fact]
    public void Convert_Conversions()
    {
        Assert.Equal(5L, TestLong.Convert(5));
        Assert.Equal(5L, TestLong.Convert(5.0f));
        Assert.Equal(1L, TestLong.Convert(true));
        Assert.Equal(0L, TestLong.Convert(false));
    }

    public sealed record TestLong : LongValueObject
    {
        public TestLong(long value) : base(value) { }

        public static new long Convert(string value) => LongValueObject.Convert(value);

        public static new long Convert(int value) => LongValueObject.Convert(value);

        public static new long Convert(float value) => LongValueObject.Convert(value);

        public static new long Convert(double value) => LongValueObject.Convert(value);

        public static new long Convert(decimal value) => LongValueObject.Convert(value);

        public static new long Convert(bool value) => LongValueObject.Convert(value);
    }
}
