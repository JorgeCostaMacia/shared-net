using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DecimalValueObjectTests
{
    // Regression: int/long funnel through (decimal) -> root Convert(decimal); must not self-recurse.
    [Fact]
    public void Create_FromIntAndLong_DoesNotRecurse()
    {
        Assert.Equal(5m, DecimalValueObject.Create(5).Value);
        Assert.Equal(5m, DecimalValueObject.Create(5L).Value);
    }

    [Fact]
    public void Create_Conversions()
    {
        Assert.Equal(100m, DecimalValueObject.Create("100").Value);
        Assert.Equal(7m, DecimalValueObject.Create(7).Value);
        Assert.Equal(1m, DecimalValueObject.Create(true).Value);
    }

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        decimal value = DecimalValueObject.Create(1.5m);
        Assert.Equal(1.5m, value);
    }
}
