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
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(PageSizeValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(PageSizeValueObject.FromOrNull(10));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(PageSizeValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(PageSizeValueObject.CreateOrNull(10));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<PageSizeValueObjectValidationException>(() => PageSizeValueObject.CreateOrNull(0));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("50", PageSizeValueObject.From(50).ToString());

    // The ceiling division is the point: the last, partial page must survive. An integer division
    // written from memory (count / size) silently drops it.
    [Fact]
    public void Pages_KeepsThePartialLastPage()
        => Assert.Equal(3, PageSizeValueObject.From(10).Pages(21));

    [Fact]
    public void Pages_OnAnExactMultiple_DoesNotAddAnEmptyPage()
        => Assert.Equal(2, PageSizeValueObject.From(10).Pages(20));

    [Fact]
    public void Pages_OnNoRows_IsZero()
        => Assert.Equal(0, PageSizeValueObject.From(10).Pages(0));

    [Fact]
    public void Pages_OnFewerRowsThanOnePage_IsOne()
        => Assert.Equal(1, PageSizeValueObject.From(10).Pages(1));
}
