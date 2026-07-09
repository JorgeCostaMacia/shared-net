using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class FloatValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw()
        => Assert.Equal(2.5f, new FloatValueObject(2.5f).Value);

    [Fact]
    public void From_KeepsValue()
        => Assert.Equal(2.5f, FloatValueObject.From(2.5f).Value);

    // The validator has no rules, so Create never throws.
    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal(2.5f, FloatValueObject.Create(2.5f).Value);

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        float value = FloatValueObject.Create(2.5f);
        Assert.Equal(2.5f, value);
    }

    // The protected Convert family is the toolbox for derived VOs in consuming contexts —
    // exercised through a derived test type, like a real derived VO would.
    [Fact]
    public void Convert_FromString_ParsesNumber_InvariantCulture()
        => Assert.Equal(3.5f, TestFloat.Convert("3.5"));

    [Fact]
    public void Convert_Conversions()
    {
        // Regression: int/long funnel through (float) -> root Convert(float); must not self-recurse.
        Assert.Equal(5f, TestFloat.Convert(5));
        Assert.Equal(5f, TestFloat.Convert(5L));
        Assert.Equal(2f, TestFloat.Convert(2m));
        Assert.Equal(2.5f, TestFloat.Convert(2.5d));
        Assert.Equal(1f, TestFloat.Convert(true));
        Assert.Equal(0f, TestFloat.Convert(false));
    }

    public sealed record TestFloat : FloatValueObject
    {
        public TestFloat(float value) : base(value) { }

        public new static float Convert(string value) => FloatValueObject.Convert(value);

        public new static float Convert(int value) => FloatValueObject.Convert(value);

        public new static float Convert(decimal value) => FloatValueObject.Convert(value);

        public new static float Convert(bool value) => FloatValueObject.Convert(value);

        public new static float Convert(long value) => FloatValueObject.Convert(value);

        public new static float Convert(double value) => FloatValueObject.Convert(value);
    }
}
