using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DecimalValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw()
        => Assert.Equal(1.5m, new DecimalValueObject(1.5m).Value);

    [Fact]
    public void From_KeepsValue()
        => Assert.Equal(1.5m, DecimalValueObject.From(1.5m).Value);

    // The validator has no rules, so Create never throws.
    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal(1.5m, DecimalValueObject.Create(1.5m).Value);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        decimal value = DecimalValueObject.Create(1.5m);
        Assert.Equal(1.5m, value);
    }

    // The protected Convert family is the toolbox for derived VOs in consuming contexts —
    // exercised through a derived test type, like a real derived VO would.
    [Fact]
    public void Convert_FromString_ParsesNumber_InvariantCulture()
        => Assert.Equal(100.5m, TestDecimal.Convert("100.5"));

    [Fact]
    public void Convert_Conversions()
    {
        // Regression: int/long funnel through (decimal) -> root Convert(decimal); must not self-recurse.
        Assert.Equal(5m, TestDecimal.Convert(5));
        Assert.Equal(5m, TestDecimal.Convert(5L));
        Assert.Equal(2.5m, TestDecimal.Convert(2.5f));
        Assert.Equal(2.5m, TestDecimal.Convert(2.5d));
        Assert.Equal(1m, TestDecimal.Convert(true));
        Assert.Equal(0m, TestDecimal.Convert(false));
    }

    public sealed record TestDecimal : DecimalValueObject
    {
        public TestDecimal(decimal value) : base(value) { }

        public static new decimal Convert(string value) => DecimalValueObject.Convert(value);

        public static new decimal Convert(int value) => DecimalValueObject.Convert(value);

        public static new decimal Convert(float value) => DecimalValueObject.Convert(value);

        public static new decimal Convert(bool value) => DecimalValueObject.Convert(value);

        public static new decimal Convert(long value) => DecimalValueObject.Convert(value);

        public static new decimal Convert(double value) => DecimalValueObject.Convert(value);
    }
}
