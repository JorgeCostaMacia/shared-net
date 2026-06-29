namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// An immutable UTC <b>Value Object</b>: a <see cref="DateTimeValueObject"/> specialization whose value always
/// carries <see cref="DateTimeKind.Utc"/>.
/// </summary>
/// <remarks>
/// <para>
/// The bare factories <b>assume the supplied value already represents UTC</b> and only tag it as such (via
/// <see cref="DateTime.SpecifyKind(DateTime, DateTimeKind)"/> in <c>Convert</c>) — they never shift the time.
/// To convert a wall-clock value from a known time zone into UTC, use the overloads that take a
/// <see cref="TimeZoneInfo"/>.
/// </para>
/// <para>
/// The constructor stores the value <b>as-is</b> (infrastructure path, e.g. ORM materialization); the static
/// <c>Create</c> factories are the ones that apply the UTC tagging.
/// </para>
/// </remarks>
public record DateTimeUtcValueObject : DateTimeValueObject
{
    /// <summary>
    /// <b>Infrastructure constructor.</b> Stores the value as-is, bypassing normalization.
    /// Use the static <c>Create</c> factories to obtain a UTC-tagged value.
    /// </summary>
    /// <param name="value">The DateTime value to encapsulate.</param>
    public DateTimeUtcValueObject(DateTime value) : base(value) { }

    // Bare factories — assume the value already represents UTC (tagged via Convert, not shifted).

    /// <summary>Creates a UTC value object from a <see cref="DateTime"/>, assuming it already represents UTC.</summary>
    public static new DateTimeUtcValueObject Create(DateTime value) => new(Convert(value));

    /// <summary>Creates a UTC value object by combining a date and a time <see cref="DateTime"/>, assuming UTC.</summary>
    public static new DateTimeUtcValueObject Create(DateTime valueDate, DateTime valueTime) => new(Convert(Convert(valueDate, valueTime)));

    /// <summary>Creates a UTC value object from a <see cref="DateOnly"/> and a <see cref="TimeOnly"/>, assuming UTC.</summary>
    public static new DateTimeUtcValueObject Create(DateOnly valueDate, TimeOnly valueTime) => new(Convert(Convert(valueDate, valueTime)));

    /// <summary>Creates a UTC value object from a date string and a time string, assuming UTC.</summary>
    public static new DateTimeUtcValueObject Create(string valueDate, string valueTime) => new(Convert(Convert(valueDate, valueTime)));

    /// <summary>Creates a UTC value object by parsing a string, assuming it already represents UTC.</summary>
    public static new DateTimeUtcValueObject Create(string value) => new(Convert(Convert(value)));

    /// <summary>Creates a UTC value object from an integer interpreted as Ticks.</summary>
    public static new DateTimeUtcValueObject Create(int value) => new(Convert(Convert(value)));

    /// <summary>Creates a UTC value object from a float (cast to integer Ticks).</summary>
    public static new DateTimeUtcValueObject Create(float value) => new(Convert(Convert(value)));

    /// <summary>Creates a UTC value object from a decimal (cast to integer Ticks).</summary>
    public static new DateTimeUtcValueObject Create(decimal value) => new(Convert(Convert(value)));

    // Conversion factories — interpret the value as a wall-clock time in the given zone and convert it to UTC.

    /// <summary>Converts a <see cref="DateTime"/> expressed in <paramref name="fromTimeZone"/> to UTC.</summary>
    public static DateTimeUtcValueObject Create(DateTime value, TimeZoneInfo fromTimeZone) => new(Convert(value, fromTimeZone));

    /// <summary>Converts a combined date and time, expressed in <paramref name="fromTimeZone"/>, to UTC.</summary>
    public static DateTimeUtcValueObject Create(DateTime valueDate, DateTime valueTime, TimeZoneInfo fromTimeZone) => new(Convert(Convert(valueDate, valueTime), fromTimeZone));

    /// <summary>Converts a <see cref="DateOnly"/>/<see cref="TimeOnly"/>, expressed in <paramref name="fromTimeZone"/>, to UTC.</summary>
    public static DateTimeUtcValueObject Create(DateOnly valueDate, TimeOnly valueTime, TimeZoneInfo fromTimeZone) => new(Convert(Convert(valueDate, valueTime), fromTimeZone));

    /// <summary>Converts a date string and a time string, expressed in <paramref name="fromTimeZone"/>, to UTC.</summary>
    public static DateTimeUtcValueObject Create(string valueDate, string valueTime, TimeZoneInfo fromTimeZone) => new(Convert(Convert(valueDate, valueTime), fromTimeZone));

    /// <summary>Parses a string expressed in <paramref name="fromTimeZone"/> and converts it to UTC.</summary>
    public static DateTimeUtcValueObject Create(string value, TimeZoneInfo fromTimeZone) => new(Convert(Convert(value), fromTimeZone));

    /// <summary>
    /// Tags the value as <see cref="DateTimeKind.Utc"/> without shifting it (assumes it already represents UTC).
    /// </summary>
    protected static new DateTime Convert(DateTime value) => DateTime.SpecifyKind(value, DateTimeKind.Utc);

    /// <summary>
    /// Treats <paramref name="value"/> as a local time in <paramref name="fromTimeZone"/> and converts it to UTC.
    /// If the value falls in a daylight-saving spring-forward gap (a non-existent local time), it is shifted
    /// forward by the zone's daylight delta so it lands on a valid instant.
    /// </summary>
    protected static DateTime Convert(DateTime value, TimeZoneInfo fromTimeZone)
    {
        DateTime local = DateTime.SpecifyKind(value, DateTimeKind.Unspecified);

        if (fromTimeZone.IsInvalidTime(local))
        {
            local = local.Add(DaylightDelta(fromTimeZone, local));
        }

        return TimeZoneInfo.ConvertTimeToUtc(local, fromTimeZone);
    }

    /// <summary>
    /// Returns the daylight-saving delta of <paramref name="fromTimeZone"/> applicable to <paramref name="value"/>
    /// (used to skip a spring-forward gap). Falls back to one hour.
    /// </summary>
    private static TimeSpan DaylightDelta(TimeZoneInfo fromTimeZone, DateTime value)
    {
        foreach (TimeZoneInfo.AdjustmentRule rule in fromTimeZone.GetAdjustmentRules())
        {
            if (value.Date >= rule.DateStart && value.Date <= rule.DateEnd)
            {
                return rule.DaylightDelta;
            }
        }

        return TimeSpan.FromHours(1);
    }
}
