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

    // Total functions: every input gets an answer, so a caller never has to guard or catch. A count
    // below zero reads as no rows.
    [Fact]
    public void Pages_OnANegativeCount_IsZero()
        => Assert.Equal(0, PageSizeValueObject.From(10).Pages(-1));

    // The off-by-one is the point: the first page skips nothing, and every page after it skips whole
    // pages. Multiplying the page number instead of the page before it skips one page too many.
    [Fact]
    public void Offset_OnTheFirstPage_SkipsNothing()
        => Assert.Equal(0, PageSizeValueObject.From(10).Offset(1));

    [Fact]
    public void Offset_OnTheSecondPage_SkipsOnePage()
        => Assert.Equal(10, PageSizeValueObject.From(10).Offset(2));

    [Fact]
    public void Offset_OnALaterPage_SkipsEveryPageBefore()
        => Assert.Equal(40, PageSizeValueObject.From(10).Offset(5));

    // Below the first page there is nothing to skip.
    [Fact]
    public void Offset_BelowTheFirstPage_SkipsNothing()
        => Assert.Equal(0, PageSizeValueObject.From(10).Offset(0));

    // The two directions meet: the offset of the last page plus one page covers the whole count.
    [Fact]
    public void Offset_OfTheLastPage_LeavesOnlyThatPage()
    {
        PageSizeValueObject size = PageSizeValueObject.From(10);

        Assert.Equal(20, size.Offset(size.Pages(21)));
    }
}
