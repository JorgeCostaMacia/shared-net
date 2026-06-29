using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class PageSizeValueObjectTests
{
    [Fact]
    public void Create_FromInt_KeepsValue()
        => Assert.Equal(50, PageSizeValueObject.Create(50).Value);

    [Fact]
    public void Create_Default_IsTenThousand()
        => Assert.Equal(10000, PageSizeValueObject.Create().Value);
}
