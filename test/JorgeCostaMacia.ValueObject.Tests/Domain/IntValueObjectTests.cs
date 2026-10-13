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

    // The protected Convert family is the toolbox for derived VOs in consuming contexts —
    // exercised through a derived test type, like a real derived VO would.
    [Fact]
    public void Convert_FromString_ParsesNumber()
        => Assert.Equal(42, TestInt.Convert("42"));

    [Fact]
    public void Convert_FromString_LargeValues_KeepFullPrecision()
    {
        // Regression: a float-based parse loses precision above 2^24 and overflows near int.MaxValue.
        Assert.Equal(int.MaxValue, TestInt.Convert("2147483647"));
        Assert.Equal(16777217, TestInt.Convert("16777217"));
    }

    [Fact]
    public void Convert_FromString_AcceptsExponentNotation()
        => Assert.Equal(1000, TestInt.Convert("1E3"));

    [Fact]
    public void Convert_FromString_OutOfRange_Throws()
        => Assert.Throws<OverflowException>(() => TestInt.Convert("2147483648"));

    // A fractional part is not dropped: truncating 5.9 to 5 would invent the value. A field that wants to
    // truncate or round says so in its own Value Object.
    [Fact]
    public void Convert_FromFractional_Throws()
    {
        Assert.Throws<FormatException>(() => TestInt.Convert(5.9));
        Assert.Throws<FormatException>(() => TestInt.Convert(-5.9));
        Assert.Throws<FormatException>(() => TestInt.Convert(2.5f));
        Assert.Throws<FormatException>(() => TestInt.Convert("2.7"));
        Assert.Throws<FormatException>(() => TestInt.Convert(2.7m));
    }

    // float and double are checked on their own type, not through decimal: decimal keeps 7 and 15 significant
    // digits, so it would accept 1.0000001f as 1 and turn 123456792f into 123456800.
    [Fact]
    public void Convert_FromFloatingPoint_KeepsItsPrecision()
    {
        Assert.Equal(123456792, TestInt.Convert(123456792f));
        Assert.Throws<FormatException>(() => TestInt.Convert(1.0000001f));
        Assert.Throws<FormatException>(() => TestInt.Convert(1000000000.0000001));
    }

    [Fact]
    public void Convert_FromFloatingPoint_OutOfRange_Throws()
    {
        Assert.Throws<OverflowException>(() => TestInt.Convert(3e10f));
        Assert.Throws<OverflowException>(() => TestInt.Convert(3e10));
        Assert.Throws<OverflowException>(() => TestInt.Convert(double.PositiveInfinity));
        Assert.Throws<FormatException>(() => TestInt.Convert(float.NaN));
    }

    // An integer written with a zero fraction is still an integer.
    [Fact]
    public void Convert_FromAZeroFraction_Accepts()
    {
        Assert.Equal(5, TestInt.Convert(5.0));
        Assert.Equal(5, TestInt.Convert("5.0"));
        Assert.Equal(-5, TestInt.Convert(-5.0m));
    }

    [Fact]
    public void Convert_Conversions()
    {
        Assert.Equal(7, TestInt.Convert(7L));
        Assert.Equal(5, TestInt.Convert(5.0f));
        Assert.Equal(1, TestInt.Convert(true));
        Assert.Equal(0, TestInt.Convert(false));
    }

    public sealed record TestInt : IntValueObject
    {
        public TestInt(int value) : base(value) { }

        public static new int Convert(string value) => IntValueObject.Convert(value);

        public static new int Convert(float value) => IntValueObject.Convert(value);

        public static new int Convert(decimal value) => IntValueObject.Convert(value);

        public static new int Convert(bool value) => IntValueObject.Convert(value);

        public static new int Convert(long value) => IntValueObject.Convert(value);

        public static new int Convert(double value) => IntValueObject.Convert(value);
    }
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(IntValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(IntValueObject.FromOrNull(42));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(IntValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(IntValueObject.CreateOrNull(42));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("42", IntValueObject.From(42).ToString());
}
