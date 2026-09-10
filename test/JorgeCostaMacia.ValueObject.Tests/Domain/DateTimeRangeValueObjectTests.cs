using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeRangeValueObjectTests
{
    private static readonly DateTime _early = new DateTime(2026, 1, 1, 0, 0, 0);
    private static readonly DateTime _late = new DateTime(2026, 12, 31, 0, 0, 0);

    [Fact]
    public void Ctor_HydratesRawParts_WithoutValidating()
    {
        DateTimeRangeValueObject range = new DateTimeRangeValueObject(new DateTimeValueObject(_late), new DateTimeValueObject(_early));

        Assert.Equal(_late, range.ValueStart.Value);
        Assert.Equal(_early, range.ValueEnd.Value);
    }

    [Fact]
    public void From_SetsStartAndEnd()
    {
        DateTimeRangeValueObject range = DateTimeRangeValueObject.From(_early, _late);

        Assert.Equal(_early, range.ValueStart.Value);
        Assert.Equal(_late, range.ValueEnd.Value);
    }

    [Fact]
    public void From_OnInvalidRange_DoesNotThrow()
    {
        DateTimeRangeValueObject range = DateTimeRangeValueObject.From(_late, _early);   // start > end, still materializes

        Assert.Equal(_late, range.ValueStart.Value);
        Assert.Equal(_early, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_SetsStartAndEnd()
    {
        DateTimeRangeValueObject range = DateTimeRangeValueObject.Create(_early, _late);

        Assert.Equal(_early, range.ValueStart.Value);
        Assert.Equal(_late, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_OnInvalid_ThrowsDateTimeRangeValueObjectValidationException()
        => Assert.Throws<DateTimeRangeValueObjectValidationException>(() => DateTimeRangeValueObject.Create(_late, _early));
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(DateTimeRangeValueObject.FromOrNull(null, null));

    // A window needs both bounds, so a half-supplied one gives no window rather than half of one.
    [Fact]
    public void FromOrNull_WithOnlyOneBound_ShortCircuitsToNull()
    {
        Assert.Null(DateTimeRangeValueObject.FromOrNull(new DateTime(2026, 1, 1), null));
        Assert.Null(DateTimeRangeValueObject.FromOrNull(null, new DateTime(2026, 2, 1)));
    }

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(DateTimeRangeValueObject.FromOrNull(new DateTime(2026, 1, 1), new DateTime(2026, 2, 1)));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(DateTimeRangeValueObject.CreateOrNull(null, null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(DateTimeRangeValueObject.CreateOrNull(new DateTime(2026, 1, 1), new DateTime(2026, 2, 1)));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<DateTimeRangeValueObjectValidationException>(() => DateTimeRangeValueObject.CreateOrNull(new DateTime(2026, 2, 1), new DateTime(2026, 1, 1)));
}
