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
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(PageNumberValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(PageNumberValueObject.FromOrNull(1));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(PageNumberValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(PageNumberValueObject.CreateOrNull(1));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<PageNumberValueObjectValidationException>(() => PageNumberValueObject.CreateOrNull(0));
    // A record generates a ToString() that prints the type and its properties; the override replaces
    // it with the bare value, which is what a log line or an interpolated string should show.
    [Fact]
    public void ToString_ShowsTheBareValue()
        => Assert.Equal("3", PageNumberValueObject.From(3).ToString());

    // Within bounds against runtime data, so each outcome is pinned: inside the data it is untouched,
    // past it collapses onto the last page, and the boundary itself stays put.
    [Fact]
    public void Within_WhenThePageIsInsideTheData_KeepsIt()
        => Assert.Equal(2, PageNumberValueObject.From(2).Within(7));

    [Fact]
    public void Within_WhenThePageReachedPastTheData_BecomesTheLastPage()
        => Assert.Equal(7, PageNumberValueObject.From(99).Within(7));

    [Fact]
    public void Within_OnTheLastPage_LeavesItAlone()
        => Assert.Equal(7, PageNumberValueObject.From(7).Within(7));

    // It chooses between two numbers the caller already has, rather than producing one, so there is
    // no empty case for it to decide: no pages bounds to no pages, as Pages(0) also says.
    [Fact]
    public void Within_OnFewerThanOnePage_IsThatCount()
        => Assert.Equal(0, PageNumberValueObject.From(2).Within(0));
}
