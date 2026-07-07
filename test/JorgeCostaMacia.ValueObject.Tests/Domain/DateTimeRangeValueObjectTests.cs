using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeRangeValueObjectTests
{
    [Fact]
    public void Create_FromDateTimes_SetsStartAndEnd()
    {
        DateTime start = new(2026, 1, 1, 0, 0, 0);
        DateTime end = new(2026, 12, 31, 0, 0, 0);

        DateTimeRangeValueObject range = DateTimeRangeValueObject.Create(start, end);

        Assert.Equal(start, range.ValueStart.Value);
        Assert.Equal(end, range.ValueEnd.Value);
    }
}
