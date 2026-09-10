using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class IntRangeValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRawParts_WithoutValidating()
    {
        IntRangeValueObject range = new IntRangeValueObject(new IntValueObject(10), new IntValueObject(1));

        Assert.Equal(10, range.ValueStart.Value);
        Assert.Equal(1, range.ValueEnd.Value);
    }

    [Fact]
    public void From_SetsStartAndEnd()
    {
        IntRangeValueObject range = IntRangeValueObject.From(1, 5);

        Assert.Equal(1, range.ValueStart.Value);
        Assert.Equal(5, range.ValueEnd.Value);
    }

    [Fact]
    public void From_OnInvalidRange_DoesNotThrow()
    {
        IntRangeValueObject range = IntRangeValueObject.From(10, 1);   // start > end, still materializes

        Assert.Equal(10, range.ValueStart.Value);
        Assert.Equal(1, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_SetsStartAndEnd()
    {
        IntRangeValueObject range = IntRangeValueObject.Create(1, 5);

        Assert.Equal(1, range.ValueStart.Value);
        Assert.Equal(5, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_OnInvalid_ThrowsIntRangeValueObjectValidationException()
        => Assert.Throws<IntRangeValueObjectValidationException>(() => IntRangeValueObject.Create(10, 1));
    // A window needs both bounds, so a half-supplied one gives no window rather than half of one.
    [Fact]
    public void FromOrNull_WithOnlyOneBound_ShortCircuitsToNull()
    {
        Assert.Null(IntRangeValueObject.FromOrNull(1, null));
        Assert.Null(IntRangeValueObject.FromOrNull(null, 5));
    }

    [Fact]
    public void FromOrNull_WithBothBounds_MaterializesTheWindow()
    {
        IntRangeValueObject? valueObject = IntRangeValueObject.FromOrNull(1, 5);

        Assert.NotNull(valueObject);
        Assert.Equal(1, valueObject.ValueStart.Value);
        Assert.Equal(5, valueObject.ValueEnd.Value);
    }

    [Fact]
    public void CreateOrNull_WithNeitherBound_ShortCircuitsWithoutValidating()
        => Assert.Null(IntRangeValueObject.CreateOrNull(null, null));

    // Absence short-circuits, invalidity does not: a supplied window still has to be a valid one.
    [Fact]
    public void CreateOrNull_WithAnInvertedRange_StillThrows()
        => Assert.Throws<IntRangeValueObjectValidationException>(() => IntRangeValueObject.CreateOrNull(5, 1));

    [Fact]
    public void CreateOrNull_WithAValidRange_ReturnsTheValueObject()
        => Assert.Equal(5, IntRangeValueObject.CreateOrNull(1, 5)!.ValueEnd.Value);
}
