using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests.Domain;

public class DateTimeUtcValueObjectTests
{
    [Fact]
    public void Create_Bare_TagsUtc_WithoutShifting()
    {
        DateTime noon = new(2026, 6, 29, 12, 0, 0);   // unspecified

        DateTimeUtcValueObject valueObject = DateTimeUtcValueObject.Create(noon);

        Assert.Equal(DateTimeKind.Utc, valueObject.Value.Kind);
        Assert.Equal(12, valueObject.Value.Hour);   // tagged UTC, not shifted
    }

    [Fact]
    public void Create_FromTimeZone_ConvertsToUtc()
    {
        // Fixed UTC+2 zone (no DST) — deterministic, independent of system time zone data.
        TimeZoneInfo plus2 = TimeZoneInfo.CreateCustomTimeZone("test+2", TimeSpan.FromHours(2), "test+2", "test+2");

        DateTimeUtcValueObject valueObject = DateTimeUtcValueObject.Create(new DateTime(2026, 6, 29, 12, 0, 0), plus2);

        Assert.Equal(DateTimeKind.Utc, valueObject.Value.Kind);
        Assert.Equal(new DateTime(2026, 6, 29, 10, 0, 0, DateTimeKind.Utc), valueObject.Value);   // 12:00 (+2) => 10:00 UTC
    }

    [Fact]
    public void Create_FromTimeZone_HandlesDaylightGap_WithoutThrowing()
    {
        TimeZoneInfo madrid = TimeZoneInfo.FindSystemTimeZoneById("Europe/Madrid");
        DateTime gap = new(2026, 3, 29, 2, 30, 0);   // Spain springs forward 02:00 -> 03:00 on 2026-03-29

        Assert.True(madrid.IsInvalidTime(gap));   // sanity: this local time does not exist

        DateTimeUtcValueObject valueObject = DateTimeUtcValueObject.Create(gap, madrid);   // must not throw

        Assert.Equal(DateTimeKind.Utc, valueObject.Value.Kind);
    }
}
