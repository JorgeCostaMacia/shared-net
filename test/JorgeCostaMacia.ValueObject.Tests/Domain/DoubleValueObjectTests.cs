using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DoubleValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw()
        => Assert.Equal(2.5d, new DoubleValueObject(2.5d).Value);

    [Fact]
    public void From_KeepsValue()
        => Assert.Equal(2.5d, DoubleValueObject.From(2.5d).Value);

    // The validator has no rules, so Create never throws.
    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal(2.5d, DoubleValueObject.Create(2.5d).Value);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        double value = DoubleValueObject.Create(2.5d);
        Assert.Equal(2.5d, value);
    }

    // The protected Convert family is the toolbox for derived VOs in consuming contexts —
    // exercised through a derived test type, like a real derived VO would.
    [Fact]
    public void Convert_FromString_ParsesNumber_InvariantCulture()
        => Assert.Equal(3.5d, TestDouble.Convert("3.5"));

    [Fact]
    public void Convert_Conversions()
    {
        // Regression: int/long funnel through (double) -> root Convert(double); must not self-recurse.
        Assert.Equal(5d, TestDouble.Convert(5));
        Assert.Equal(5d, TestDouble.Convert(5L));
        Assert.Equal(2d, TestDouble.Convert(2m));
        Assert.Equal(2.5d, TestDouble.Convert(2.5f));
        Assert.Equal(1d, TestDouble.Convert(true));
        Assert.Equal(0d, TestDouble.Convert(false));
    }

    public sealed record TestDouble : DoubleValueObject
    {
        public TestDouble(double value) : base(value) { }

        public new static double Convert(string value) => DoubleValueObject.Convert(value);

        public new static double Convert(int value) => DoubleValueObject.Convert(value);

        public new static double Convert(long value) => DoubleValueObject.Convert(value);

        public new static double Convert(float value) => DoubleValueObject.Convert(value);

        public new static double Convert(decimal value) => DoubleValueObject.Convert(value);

        public new static double Convert(bool value) => DoubleValueObject.Convert(value);
    }
}
