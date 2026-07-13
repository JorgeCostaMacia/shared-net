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
    [InlineData("YES", true)]
    [InlineData("false", false)]
    [InlineData("0", false)]
    [InlineData("nope", false)]
    public void Convert_FromString_ParsesTruthyTokens(string input, bool expected)
        => Assert.Equal(expected, TestBool.Convert(input));

    [Fact]
    public void Convert_FromNumbers_OnlyOneMapsToTrue()
    {
        Assert.True(TestBool.Convert(1));
        Assert.False(TestBool.Convert(0));
        Assert.False(TestBool.Convert(2));
        Assert.True(TestBool.Convert(1L));
        Assert.True(TestBool.Convert(1.0));
        Assert.True(TestBool.Convert(1m));
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
}
