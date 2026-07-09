using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class PageSizeValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutValidating()
        => Assert.Equal(0, new PageSizeValueObject(0).Value);

    [Fact]
    public void From_KeepsValue()
        => Assert.Equal(50, PageSizeValueObject.From(50).Value);

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal(0, PageSizeValueObject.From(0).Value);

    [Fact]
    public void Create_KeepsValue()
        => Assert.Equal(50, PageSizeValueObject.Create(50).Value);

    [Fact]
    public void Create_OnInvalid_ThrowsPageSizeValueObjectValidationException()
        => Assert.Throws<PageSizeValueObjectValidationException>(() => PageSizeValueObject.Create(0));
}
