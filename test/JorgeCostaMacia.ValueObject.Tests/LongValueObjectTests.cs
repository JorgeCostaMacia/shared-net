using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class LongValueObjectTests
{
    [Fact]
    public void Create_FromInt_DoesNotRecurse()
        => Assert.Equal(5L, LongValueObject.Create(5).Value);

    [Fact]
    public void Create_Conversions()
    {
        Assert.Equal(42L, LongValueObject.Create("42").Value);
        Assert.Equal(9L, LongValueObject.Create(9L).Value);
        Assert.Equal(5L, LongValueObject.Create(5.0d).Value);
        Assert.Equal(5L, LongValueObject.Create(5m).Value);
        Assert.Equal(1L, LongValueObject.Create(true).Value);
    }
}
