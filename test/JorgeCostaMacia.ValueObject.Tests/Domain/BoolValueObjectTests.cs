using System.Globalization;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class BoolValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw()
        => Assert.True(new BoolValueObject(true).Value);

    [Fact]
    public void From_KeepsValue()
    {
        Assert.True(BoolValueObject.From(true).Value);
        Assert.False(BoolValueObject.From(false).Value);
    }

    // The validator has no rules, so Create never throws.
    [Fact]
    public void Create_KeepsValue()
    {
        Assert.True(BoolValueObject.Create(true).Value);
        Assert.False(BoolValueObject.Create(false).Value);
    }

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        bool value = BoolValueObject.Create(true);
        Assert.True(value);
    }

    // The protected Convert family is the toolbox for derived VOs in consuming contexts —
    // exercised through a derived test type, like a real derived VO would.
    [Theory]
    [InlineData("TRUE", true)]
    [InlineData("true", true)]
    [InlineData("1", true)]
    [InlineData("SI", true)]
    [InlineData("sí", true)]
    [InlineData("S", true)]
    [InlineData(" yes ", true)]
    [InlineData("Y", true)]
    [InlineData("FALSE", false)]
    [InlineData("false", false)]
    [InlineData("0", false)]
    [InlineData("no", false)]
    [InlineData("N", false)]
    public void Convert_FromString_ParsesTheKnownTokens(
        string input,
        bool expected)
        => Assert.Equal(expected, TestBool.Convert(input));

    // A token on neither list is not read as false: "S" used to be, which inverts every S/N field.
    [Theory]
    [InlineData("")]
    [InlineData("nope")]
    [InlineData("2")]
    [InlineData("1.0")]
    public void Convert_FromAnUnknownToken_Throws(string input)
        => Assert.Throws<FormatException>(() => TestBool.Convert(input));

    // The tokens are compared upper-cased with the invariant culture: under tr-TR, ToUpper turns "si" into "Sİ".
    [Fact]
    public void Convert_FromString_IndependentlyOfTheCulture()
    {
        CultureInfo culture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

        try
        {
            Assert.True(TestBool.Convert("si"));
        }
        finally
        {
            CultureInfo.CurrentCulture = culture;
        }
    }

    [Fact]
    public void Convert_FromNumbers_OneIsTrueAndZeroIsFalse()
    {
        Assert.True(TestBool.Convert(1));
        Assert.False(TestBool.Convert(0));
        Assert.True(TestBool.Convert(1L));
        Assert.False(TestBool.Convert(0L));
        Assert.True(TestBool.Convert(1f));
        Assert.False(TestBool.Convert(0f));
        Assert.True(TestBool.Convert(1.0));
        Assert.False(TestBool.Convert(0.0));
        Assert.True(TestBool.Convert(1m));
        Assert.False(TestBool.Convert(0m));
    }

    // Any other number is not a boolean: 2 used to be false and 1.9 true.
    [Fact]
    public void Convert_FromAnyOtherNumber_Throws()
    {
        Assert.Throws<FormatException>(() => TestBool.Convert(2));
        Assert.Throws<FormatException>(() => TestBool.Convert(-1L));
        Assert.Throws<FormatException>(() => TestBool.Convert(1.9f));
        Assert.Throws<FormatException>(() => TestBool.Convert(1.0000000000000002));
        Assert.Throws<FormatException>(() => TestBool.Convert(0.5m));
        Assert.Throws<FormatException>(() => TestBool.Convert(double.NaN));
    }

    public sealed record TestBool : BoolValueObject
    {
        public TestBool(bool value) : base(value) { }

        public static new bool Convert(string value) => BoolValueObject.Convert(value);

        public static new bool Convert(int value) => BoolValueObject.Convert(value);

        public static new bool Convert(float value) => BoolValueObject.Convert(value);

        public static new bool Convert(long value) => BoolValueObject.Convert(value);

        public static new bool Convert(double value) => BoolValueObject.Convert(value);

        public static new bool Convert(decimal value) => BoolValueObject.Convert(value);
    }
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(BoolValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(BoolValueObject.FromOrNull(true));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(BoolValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(BoolValueObject.CreateOrNull(true));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("True", BoolValueObject.From(true).ToString());
}
