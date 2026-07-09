using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeRangeValueObjectTests
{
    private static readonly DateTime Early = new(2026, 1, 1, 0, 0, 0);
    private static readonly DateTime Late = new(2026, 12, 31, 0, 0, 0);

    [Fact]
    public void Ctor_HydratesRawParts_WithoutValidating()
    {
        DateTimeRangeValueObject range = new DateTimeRangeValueObject(new DateTimeValueObject(Late), new DateTimeValueObject(Early));

        Assert.Equal(Late, range.ValueStart.Value);
        Assert.Equal(Early, range.ValueEnd.Value);
    }

    [Fact]
    public void From_SetsStartAndEnd()
    {
        DateTimeRangeValueObject range = DateTimeRangeValueObject.From(Early, Late);

        Assert.Equal(Early, range.ValueStart.Value);
        Assert.Equal(Late, range.ValueEnd.Value);
    }

    [Fact]
    public void From_OnInvalidRange_DoesNotThrow()
    {
        DateTimeRangeValueObject range = DateTimeRangeValueObject.From(Late, Early);   // start > end, still materializes

        Assert.Equal(Late, range.ValueStart.Value);
        Assert.Equal(Early, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_SetsStartAndEnd()
    {
        DateTimeRangeValueObject range = DateTimeRangeValueObject.Create(Early, Late);

        Assert.Equal(Early, range.ValueStart.Value);
        Assert.Equal(Late, range.ValueEnd.Value);
    }

    [Fact]
    public void Create_OnInvalid_ThrowsDateTimeRangeValueObjectValidationException()
        => Assert.Throws<DateTimeRangeValueObjectValidationException>(() => DateTimeRangeValueObject.Create(Late, Early));
}
