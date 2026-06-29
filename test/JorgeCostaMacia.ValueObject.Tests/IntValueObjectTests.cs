using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class IntValueObjectTests
{
    [Fact]
    public void Create_FromString_ParsesNumber()
        => Assert.Equal(42, IntValueObject.Create("42").Value);

    [Fact]
    public void Create_Conversions()
    {
        Assert.Equal(7, IntValueObject.Create(7).Value);
        Assert.Equal(7, IntValueObject.Create(7L).Value);
        Assert.Equal(5, IntValueObject.Create(5.0f).Value);
        Assert.Equal(5, IntValueObject.Create(5.0).Value);
        Assert.Equal(5, IntValueObject.Create(5m).Value);
        Assert.Equal(1, IntValueObject.Create(true).Value);
        Assert.Equal(0, IntValueObject.Create(false).Value);
    }
}
