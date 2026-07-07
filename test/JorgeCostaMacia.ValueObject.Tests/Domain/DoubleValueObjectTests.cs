using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DoubleValueObjectTests
{
    [Fact]
    public void Create_FromIntAndLong_DoesNotRecurse()
    {
        Assert.Equal(5d, DoubleValueObject.Create(5).Value);
        Assert.Equal(5d, DoubleValueObject.Create(5L).Value);
    }

    [Fact]
    public void Create_Conversions()
    {
        Assert.Equal(3d, DoubleValueObject.Create("3").Value);
        Assert.Equal(2.5d, DoubleValueObject.Create(2.5d).Value);
        Assert.Equal(2d, DoubleValueObject.Create(2m).Value);
        Assert.Equal(1d, DoubleValueObject.Create(true).Value);
    }
}
