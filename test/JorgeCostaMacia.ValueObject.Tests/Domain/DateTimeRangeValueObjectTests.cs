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
}
