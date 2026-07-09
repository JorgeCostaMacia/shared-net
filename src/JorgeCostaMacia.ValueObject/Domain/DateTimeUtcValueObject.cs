using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// An immutable UTC <b>Value Object</b>: a <see cref="DateTimeValueObject"/> specialization whose value always
/// carries <see cref="DateTimeKind.Utc"/>.
/// </summary>
/// <remarks>
/// <para>
/// The factories <b>assume the supplied value already represents UTC</b> and only tag it as such (via
/// <see cref="DateTime.SpecifyKind(DateTime, DateTimeKind)"/> in <see cref="Convert(DateTime)"/>) — they never
/// shift the time. To convert a wall-clock value from a known time zone into UTC, derived types can use the
/// protected <see cref="Convert(DateTime, TimeZoneInfo)"/> helper, which handles daylight-saving gaps.
/// </para>
/// <para>
/// It exposes the three-verb creation surface: the constructor hydrates (stores the value <b>as-is</b>, e.g.
/// ORM materialization), <see cref="From(DateTime)"/> converts (tags UTC, unvalidated) and
/// <see cref="Create(DateTime)"/> fabricates validated.
/// </para>
/// </remarks>
public record DateTimeUtcValueObject : DateTimeValueObject
{
    /// <summary>
    /// <b>Hydration Constructor.</b> Stores the value as-is, bypassing normalization and validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The DateTime value to encapsulate.</param>
    public DateTimeUtcValueObject(DateTime value) : base(value) { }

    /// <summary>
    /// Converts: tags the value as <see cref="DateTimeKind.Utc"/> through <see cref="Convert(DateTime)"/>
    /// <b>without shifting it</b> (assumes it already represents UTC) and materializes a new
    /// <see cref="DateTimeUtcValueObject"/>, unvalidated. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source <see cref="DateTime"/> value, assumed to already represent UTC.</param>
    /// <returns>A new, UTC-tagged but unvalidated <see cref="DateTimeUtcValueObject"/> instance.</returns>
    public new static DateTimeUtcValueObject From(DateTime value) => new DateTimeUtcValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(DateTime)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source <see cref="DateTime"/> value, assumed to already represent UTC.</param>
    /// <returns>A new, validated <see cref="DateTimeUtcValueObject"/> instance.</returns>
    /// <exception cref="DateTimeUtcValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public new static DateTimeUtcValueObject Create(DateTime value)
    {
        DateTimeUtcValueObject vo = From(value);
        DateTimeUtcValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Tags the value as <see cref="DateTimeKind.Utc"/> without shifting it (assumes it already represents UTC).
    /// </summary>
    protected static new DateTime Convert(DateTime value) => DateTime.SpecifyKind(value, DateTimeKind.Utc);

    /// <summary>
    /// Treats <paramref name="value"/> as a local time in <paramref name="fromTimeZone"/> and converts it to UTC.
    /// If the value falls in a daylight-saving spring-forward gap (a non-existent local time), it is shifted
    /// forward by the zone's daylight delta so it lands on a valid instant.
    /// </summary>
    /// <param name="value">The wall-clock value to convert.</param>
    /// <param name="fromTimeZone">The time zone the wall-clock value is expressed in.</param>
    /// <returns>The equivalent UTC <see cref="DateTime"/>.</returns>
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
    /// <param name="fromTimeZone">The time zone whose adjustment rules are inspected.</param>
    /// <param name="value">The value whose applicable daylight delta is looked up.</param>
    /// <returns>The applicable daylight-saving <see cref="TimeSpan"/> delta.</returns>
    protected static TimeSpan DaylightDelta(TimeZoneInfo fromTimeZone, DateTime value)
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
