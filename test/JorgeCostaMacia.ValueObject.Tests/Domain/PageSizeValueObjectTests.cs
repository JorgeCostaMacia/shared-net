using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class PageSizeValueObjectTests
{
    [Fact]
    public void Create_FromInt_KeepsValue()
        => Assert.Equal(50, PageSizeValueObject.Create(50).Value);
}
