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

    [Fact]
    public void Convert_FromFractional_TruncatesTowardZero()
    {
        Assert.Equal(5, TestInt.Convert(5.9));      // not rounded up
        Assert.Equal(2, TestInt.Convert(2.5));      // not banker's-rounded
        Assert.Equal(3, TestInt.Convert(3.5));      // truncated, not rounded to 4
        Assert.Equal(-5, TestInt.Convert(-5.9));    // toward zero
        Assert.Equal(5, TestInt.Convert("5.9"));
        Assert.Equal(5, TestInt.Convert(5.9m));
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
}
