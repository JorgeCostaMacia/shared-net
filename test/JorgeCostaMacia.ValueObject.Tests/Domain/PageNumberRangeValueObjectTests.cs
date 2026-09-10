using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class PageNumberRangeValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRawParts_WithoutValidating()
    {
        PageNumberRangeValueObject range = new PageNumberRangeValueObject(new PageNumberValueObject(0), new PageNumberValueObject(-1));

        Assert.Equal(0, range.ValueStart.Value);
        Assert.Equal(-1, range.ValueEnd.Value);
    }

    [Fact]
    public void From_SetsStartAndEnd()
    {
        PageNumberRangeValueObject range = PageNumberRangeValueObject.From(2, 10);

        Assert.Equal(2, range.ValueStart.Value);
        Assert.Equal(10, range.ValueEnd.Value);
    }

    [Fact]
    public void From_OnInvalidRange_DoesNotThrow()
    {
        PageNumberRangeValueObject range = PageNumberRangeValueObject.From(0, -1);   // invalid pages, still materializes

        Assert.Equal(0, range.ValueStart.Value);
        Assert.Equal(-1, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_SetsStartAndEnd()
    {
        PageNumberRangeValueObject range = PageNumberRangeValueObject.Create(2, 10);

        Assert.Equal(2, range.ValueStart.Value);
        Assert.Equal(10, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_OnInvalid_ThrowsPageNumberRangeValueObjectValidationException()
        => Assert.Throws<PageNumberRangeValueObjectValidationException>(() => PageNumberRangeValueObject.Create(10, 1));

    [Fact]
    public void Create_OnInvalid_ReportsAllFailuresInOneException()
    {
        // (0, -1) violates the part rules (both pages must be > 0) and the range invariant in both
        // directions — ONE composed pass, one exception, the complete failure list.
        PageNumberRangeValueObjectValidationException exception =
            Assert.Throws<PageNumberRangeValueObjectValidationException>(() => PageNumberRangeValueObject.Create(0, -1));

        Assert.Equal(4, exception.Validations.Count);
        Assert.Contains(exception.Validations, v => v.PropertyName == "ValueStart.Value");
        Assert.Contains(exception.Validations, v => v.PropertyName == "ValueEnd.Value");
    }
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(PageNumberRangeValueObject.FromOrNull(null, null));

    // A window needs both bounds, so a half-supplied one gives no window rather than half of one.
    [Fact]
    public void FromOrNull_WithOnlyOneBound_ShortCircuitsToNull()
    {
        Assert.Null(PageNumberRangeValueObject.FromOrNull(1, null));
        Assert.Null(PageNumberRangeValueObject.FromOrNull(null, 5));
    }

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(PageNumberRangeValueObject.FromOrNull(1, 5));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(PageNumberRangeValueObject.CreateOrNull(null, null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(PageNumberRangeValueObject.CreateOrNull(1, 5));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<PageNumberRangeValueObjectValidationException>(() => PageNumberRangeValueObject.CreateOrNull(5, 1));
}
