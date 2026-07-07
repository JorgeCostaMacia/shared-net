using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeValueObjectTests
{
    [Fact]
    public void Create_PreservesKind_DoesNotForceUtc()
    {
        DateTime local = new(2026, 6, 29, 12, 0, 0, DateTimeKind.Local);

        DateTimeValueObject valueObject = DateTimeValueObject.Create(local);

        Assert.Equal(DateTimeKind.Local, valueObject.Value.Kind);   // not forced to UTC
        Assert.Equal(local, valueObject.Value);                     // not shifted
    }

    [Fact]
    public void Create_FromUnspecified_StaysUnspecified()
    {
        DateTime unspecified = new(2026, 6, 29, 12, 0, 0);

        Assert.Equal(DateTimeKind.Unspecified, DateTimeValueObject.Create(unspecified).Value.Kind);
    }

    [Fact]
    public void Create_FromDateAndTime_TakesDateFromFirstAndTimeFromSecond()
    {
        DateTime date = new(2026, 3, 15, 9, 8, 7);
        DateTime time = new(2001, 1, 1, 14, 30, 45);

        DateTimeValueObject result = DateTimeValueObject.Create(date, time);

        Assert.Equal(new DateTime(2026, 3, 15, 14, 30, 45), result.Value);   // hour is 14 (from time), not 9 (from date)
    }

    [Fact]
    public void Create_FromDateOnlyAndTimeOnly_Combines()
    {
        DateOnly date = new(2026, 3, 15);
        TimeOnly time = new(14, 30, 45);

        Assert.Equal(new DateTime(2026, 3, 15, 14, 30, 45), DateTimeValueObject.Create(date, time).Value);
    }
}
