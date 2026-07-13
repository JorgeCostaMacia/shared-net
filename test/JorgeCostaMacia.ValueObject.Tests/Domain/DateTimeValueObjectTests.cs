using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutValidating()
        => Assert.Equal(default, new DateTimeValueObject(default).Value);

    [Fact]
    public void From_PreservesKind_DoesNotForceUtc()
    {
        DateTime local = new DateTime(2026, 6, 29, 12, 0, 0, DateTimeKind.Local);

        DateTimeValueObject valueObject = DateTimeValueObject.From(local);

        Assert.Equal(DateTimeKind.Local, valueObject.Value.Kind);   // not forced to UTC
        Assert.Equal(local, valueObject.Value);                     // not shifted
    }

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal(default, DateTimeValueObject.From(default).Value);

    [Fact]
    public void Create_PreservesKind_DoesNotForceUtc()
    {
        DateTime local = new DateTime(2026, 6, 29, 12, 0, 0, DateTimeKind.Local);

        DateTimeValueObject valueObject = DateTimeValueObject.Create(local);

        Assert.Equal(DateTimeKind.Local, valueObject.Value.Kind);
        Assert.Equal(local, valueObject.Value);
    }

    [Fact]
    public void Create_FromUnspecified_StaysUnspecified()
    {
        DateTime unspecified = new DateTime(2026, 6, 29, 12, 0, 0);

        Assert.Equal(DateTimeKind.Unspecified, DateTimeValueObject.Create(unspecified).Value.Kind);
    }

    [Fact]
    public void Create_OnInvalid_ThrowsDateTimeValueObjectValidationException()
        => Assert.Throws<DateTimeValueObjectValidationException>(() => DateTimeValueObject.Create(new DateTime(1800, 1, 1)));

    [Fact]
    public void Create_OnDefault_ReportsAllFailuresInOneException()
    {
        // default(DateTime) violates both NotEmpty and the 1900 minimum — one exception, the complete failure list.
        DateTimeValueObjectValidationException exception =
            Assert.Throws<DateTimeValueObjectValidationException>(() => DateTimeValueObject.Create(default));

        Assert.Equal(2, exception.Validations.Count);
    }

    [Fact]
    public void ImplicitOperator_ReturnsUnderlyingValue()
    {
        DateTime moment = new DateTime(2020, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        DateTime value = DateTimeValueObject.Create(moment);
        Assert.Equal(moment, value);
    }

    // The protected Convert family is the toolbox for derived VOs in consuming contexts —
    // exercised through a derived test type, like a real derived VO would.
    [Fact]
    public void Convert_FromDateAndTime_TakesDateFromFirstAndTimeFromSecond()
    {
        DateTime date = new DateTime(2026, 3, 15, 9, 8, 7);
        DateTime time = new DateTime(2001, 1, 1, 14, 30, 45);

        Assert.Equal(new DateTime(2026, 3, 15, 14, 30, 45), TestDateTime.Convert(date, time));   // hour is 14 (from time), not 9 (from date)
    }

    [Fact]
    public void Convert_FromDateOnlyAndTimeOnly_Combines()
    {
        DateOnly date = new DateOnly(2026, 3, 15);
        TimeOnly time = new TimeOnly(14, 30, 45);

        Assert.Equal(new DateTime(2026, 3, 15, 14, 30, 45), TestDateTime.Convert(date, time));
    }

    [Fact]
    public void Convert_FromString_Parses_InvariantCulture()
        => Assert.Equal(new DateTime(2026, 3, 15, 14, 30, 45), TestDateTime.Convert("2026-03-15T14:30:45"));

    [Fact]
    public void Convert_FromDateAndTimeStrings_Combines()
        => Assert.Equal(new DateTime(2026, 3, 15, 14, 30, 45), TestDateTime.Convert("2026-03-15", "14:30:45"));

    public sealed record TestDateTime : DateTimeValueObject
    {
        public TestDateTime(DateTime value) : base(value) { }

        public static new DateTime Convert(DateTime valueDate, DateTime valueTime) => DateTimeValueObject.Convert(valueDate, valueTime);

        public static new DateTime Convert(DateOnly valueDate, TimeOnly valueTime) => DateTimeValueObject.Convert(valueDate, valueTime);

        public static new DateTime Convert(string value) => DateTimeValueObject.Convert(value);

        public static new DateTime Convert(string valueDate, string valueTime) => DateTimeValueObject.Convert(valueDate, valueTime);
    }
}
