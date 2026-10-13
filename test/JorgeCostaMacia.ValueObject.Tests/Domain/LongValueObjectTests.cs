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

    // A fractional part is not dropped: truncating 5.9 to 5 would invent the value. A field that wants to
    // truncate or round says so in its own Value Object.
    [Fact]
    public void Convert_FromFractional_Throws()
    {
        Assert.Throws<FormatException>(() => TestLong.Convert(5.9));
        Assert.Throws<FormatException>(() => TestLong.Convert(-5.9));
        Assert.Throws<FormatException>(() => TestLong.Convert(2.5f));
        Assert.Throws<FormatException>(() => TestLong.Convert("2.7"));
        Assert.Throws<FormatException>(() => TestLong.Convert(2.7m));
    }

    // An integer written with a zero fraction is still an integer.
    [Fact]
    public void Convert_FromAZeroFraction_Accepts()
    {
        Assert.Equal(5L, TestLong.Convert(5.0));
        Assert.Equal(5L, TestLong.Convert("5.0"));
        Assert.Equal(-5L, TestLong.Convert(-5.0m));
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
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(LongValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(LongValueObject.FromOrNull(42L));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(LongValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(LongValueObject.CreateOrNull(42L));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("42", LongValueObject.From(42L).ToString());
}
