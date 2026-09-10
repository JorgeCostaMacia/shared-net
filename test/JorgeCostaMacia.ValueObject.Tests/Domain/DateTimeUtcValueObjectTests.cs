using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeUtcValueObjectTests
{
    [Fact]
    public void Ctor_HydratesRaw_WithoutTaggingUtc()
    {
        DateTime local = new DateTime(2026, 6, 29, 12, 0, 0, DateTimeKind.Local);

        Assert.Equal(DateTimeKind.Local, new DateTimeUtcValueObject(local).Value.Kind);   // stored as-is
    }

    [Fact]
    public void From_TagsUtc_WithoutShifting()
    {
        DateTime noon = new DateTime(2026, 6, 29, 12, 0, 0);   // unspecified

        DateTimeUtcValueObject valueObject = DateTimeUtcValueObject.From(noon);

        Assert.Equal(DateTimeKind.Utc, valueObject.Value.Kind);
        Assert.Equal(12, valueObject.Value.Hour);   // tagged UTC, not shifted
    }

    [Fact]
    public void From_OnInvalid_DoesNotThrow()
        => Assert.Equal(DateTimeKind.Utc, DateTimeUtcValueObject.From(default).Value.Kind);

    [Fact]
    public void Create_TagsUtc_WithoutShifting()
    {
        DateTime noon = new DateTime(2026, 6, 29, 12, 0, 0);   // unspecified

        DateTimeUtcValueObject valueObject = DateTimeUtcValueObject.Create(noon);

        Assert.Equal(DateTimeKind.Utc, valueObject.Value.Kind);
        Assert.Equal(12, valueObject.Value.Hour);
    }

    [Fact]
    public void Create_OnInvalid_ThrowsDateTimeUtcValueObjectValidationException()
        => Assert.Throws<DateTimeUtcValueObjectValidationException>(() => DateTimeUtcValueObject.Create(new DateTime(1800, 1, 1)));

    // Convert(value, fromTimeZone) is protected — exercised through a derived test type, like a real derived VO would.
    [Fact]
    public void Convert_FromTimeZone_ConvertsToUtc()
    {
        // Fixed UTC+2 zone (no DST) — deterministic, independent of system time zone data.
        TimeZoneInfo plus2 = TimeZoneInfo.CreateCustomTimeZone("test+2", TimeSpan.FromHours(2), "test+2", "test+2");

        DateTime result = TestUtc.ConvertToUtc(new DateTime(2026, 6, 29, 12, 0, 0), plus2);

        Assert.Equal(new DateTime(2026, 6, 29, 10, 0, 0, DateTimeKind.Utc), result);   // 12:00 (+2) => 10:00 UTC
    }

    [Fact]
    public void Convert_FromTimeZone_HandlesDaylightGap_WithoutThrowing()
    {
        TimeZoneInfo madrid = TimeZoneInfo.FindSystemTimeZoneById("Europe/Madrid");
        DateTime gap = new DateTime(2026, 3, 29, 2, 30, 0);   // Spain springs forward 02:00 -> 03:00 on 2026-03-29

        Assert.True(madrid.IsInvalidTime(gap));   // sanity: this local time does not exist

        DateTime result = TestUtc.ConvertToUtc(gap, madrid);   // must not throw

        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    public record TestUtc : DateTimeUtcValueObject
    {
        public TestUtc(DateTime value) : base(value) { }

        public static DateTime ConvertToUtc(DateTime value, TimeZoneInfo fromTimeZone) => Convert(value, fromTimeZone);
    }
    // The OrNull pair carries the field's optionality: absence in, absence out — never a second
    // policy for invalid input.
    [Fact]
    public void FromOrNull_WithNoValue_ShortCircuitsToNull()
        => Assert.Null(DateTimeUtcValueObject.FromOrNull(null));

    [Fact]
    public void FromOrNull_WithAValue_MaterializesIt()
        => Assert.NotNull(DateTimeUtcValueObject.FromOrNull(new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc)));

    [Fact]
    public void CreateOrNull_WithNoValue_ShortCircuitsWithoutValidating()
        => Assert.Null(DateTimeUtcValueObject.CreateOrNull(null));

    [Fact]
    public void CreateOrNull_WithAValue_ReturnsTheValueObject()
        => Assert.NotNull(DateTimeUtcValueObject.CreateOrNull(new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc)));

    // Absence short-circuits, invalidity does not: a supplied value still goes through the rules.
    [Fact]
    public void CreateOrNull_WithAnInvalidValue_StillThrows()
        => Assert.Throws<DateTimeUtcValueObjectValidationException>(() => DateTimeUtcValueObject.CreateOrNull(new DateTime(1800, 1, 1)));
}
