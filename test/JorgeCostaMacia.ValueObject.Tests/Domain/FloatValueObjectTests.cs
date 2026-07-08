using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class FloatValueObjectTests
{
    // Regression: int/long funnel through (float) -> root Convert(float); must not self-recurse.
    [Fact]
    public void Create_FromIntAndLong_DoesNotRecurse()
    {
        Assert.Equal(5f, FloatValueObject.Create(5).Value);
        Assert.Equal(5f, FloatValueObject.Create(5L).Value);
    }

    [Fact]
    public void Create_Conversions()
    {
        Assert.Equal(3f, FloatValueObject.Create("3").Value);
        Assert.Equal(2.5f, FloatValueObject.Create(2.5f).Value);
        Assert.Equal(2f, FloatValueObject.Create(2m).Value);
        Assert.Equal(1f, FloatValueObject.Create(true).Value);
    }

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        float value = FloatValueObject.Create(2.5f);
        Assert.Equal(2.5f, value);
    }
}
