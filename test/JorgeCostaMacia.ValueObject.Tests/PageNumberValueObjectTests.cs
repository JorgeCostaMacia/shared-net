using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

public class PageNumberValueObjectTests
{
    [Fact]
    public void Create_FromInt_KeepsValue()
        => Assert.Equal(5, PageNumberValueObject.Create(5).Value);
}
