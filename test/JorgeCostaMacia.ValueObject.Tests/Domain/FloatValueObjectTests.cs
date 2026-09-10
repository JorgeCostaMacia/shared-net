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

        public static new float Convert(string value) => FloatValueObject.Convert(value);

        public static new float Convert(int value) => FloatValueObject.Convert(value);

        public static new float Convert(decimal value) => FloatValueObject.Convert(value);

        public static new float Convert(bool value) => FloatValueObject.Convert(value);

        public static new float Convert(long value) => FloatValueObject.Convert(value);

        public static new float Convert(double value) => FloatValueObject.Convert(value);
    }
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(FloatValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(FloatValueObject.FromOrNull(1.5f));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(FloatValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(FloatValueObject.CreateOrNull(1.5f));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal(1.5f.ToString(), FloatValueObject.From(1.5f).ToString());
}
