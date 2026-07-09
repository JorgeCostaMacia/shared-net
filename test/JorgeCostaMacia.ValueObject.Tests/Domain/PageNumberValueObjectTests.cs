using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class PageNumberValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutValidating()
        => Assert.Equal(0, new PageNumberValueObject(0).Value);

    [Fact]
    public void From_KeepsValue()
        => Assert.Equal(5, PageNumberValueObject.From(5).Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal(0, PageNumberValueObject.From(0).Value);

    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal(5, PageNumberValueObject.Create(5).Value);

    [Fact]
    public void Create_OnInvalid_ThrowsPageNumberValueObjectValidationException()
        => Assert.Throws<PageNumberValueObjectValidationException>(() => PageNumberValueObject.Create(0));
}
