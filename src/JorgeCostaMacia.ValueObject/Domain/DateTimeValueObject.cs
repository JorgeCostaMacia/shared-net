using System.Globalization;
using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="DateTime"/> value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on time points (e.g., CreatedAt, EventDate).
/// It <b>preserves the supplied value's <see cref="DateTimeKind"/> as-is</b> — it does not convert the timezone.
/// Use <see cref="DateTimeUtcValueObject"/> when you need a UTC guarantee.
/// </para>
/// <para>
/// It exposes the three-verb creation surface: the constructor hydrates (raw assignment for ORMs and
/// deserializers), <see cref="From(DateTime)"/> converts (materializes, unvalidated) and
/// <see cref="Create(DateTime)"/> fabricates validated (nothing invalid escapes it).
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="DateTime"/>). The protected <c>Convert</c>
/// family carries the conversion logic from other representations (strings, date/time parts, ticks), so
/// Value Objects deriving from this one in consuming contexts can reuse and redefine it.
/// </para>
/// </remarks>
public record DateTimeValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="DateTime"/> value encapsulated by this Value Object, stored as provided.
    /// </summary>
    public DateTime Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The DateTime value to encapsulate.</param>
    public DateTimeValueObject(DateTime value)
    {
        Value = value;
    }

    /// <summary>
    /// Converts: materializes a new <see cref="DateTimeValueObject"/> from the natural primitive through
    /// <see cref="Convert(DateTime)"/>, preserving its <see cref="DateTimeKind"/> and <b>without validating it</b>.
    /// This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source <see cref="DateTime"/> value.</param>
    /// <returns>A new, unvalidated <see cref="DateTimeValueObject"/> instance.</returns>
    public static DateTimeValueObject From(DateTime value) => new DateTimeValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(DateTime)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source <see cref="DateTime"/> value.</param>
    /// <returns>A new, validated <see cref="DateTimeValueObject"/> instance.</returns>
    /// <exception cref="DateTimeValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static DateTimeValueObject Create(DateTime value)
    {
        DateTimeValueObject vo = From(value);
        DateTimeValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Returns the <see cref="DateTime"/> value as-is, preserving its <see cref="DateTimeKind"/>.
    /// </summary>
    protected static DateTime Convert(DateTime value) => value;

    /// <summary>
    /// Combines the date part of the first parameter with the time part of the second parameter.
    /// The resulting <see cref="DateTimeKind"/> is not set explicitly.
    /// </summary>
    protected static DateTime Convert(DateTime valueDate, DateTime valueTime) => valueDate.Date.AddHours(valueTime.Hour).AddMinutes(valueTime.Minute).AddSeconds(valueTime.Second).AddMilliseconds(valueTime.Millisecond).AddMicroseconds(valueTime.Microsecond);

    /// <summary>
    /// Combines a <see cref="DateOnly"/> and a <see cref="TimeOnly"/> into a <see cref="DateTime"/> (kind unspecified).
    /// </summary>
    protected static DateTime Convert(DateOnly valueDate, TimeOnly valueTime) => Convert(valueDate.ToDateTime(valueTime));

    /// <summary>
    /// Parses a string into a <see cref="DateTime"/> object, trimming whitespace first.
    /// Note: The resulting <see cref="DateTimeKind"/> depends on the input string format/culture.
    /// </summary>
    protected static DateTime Convert(string value) => Convert(DateTime.Parse(value.Trim(), CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts separate date and time strings by parsing them and combining them using <see cref="DateOnly"/> and <see cref="TimeOnly"/>.
    /// Note: The resulting <see cref="DateTimeKind"/> is not set explicitly.
    /// </summary>
    protected static DateTime Convert(string valueDate, string valueTime) => Convert(DateOnly.FromDateTime(DateTime.Parse(valueDate.Trim(), CultureInfo.InvariantCulture)).ToDateTime(TimeOnly.FromDateTime(DateTime.Parse(valueTime.Trim(), CultureInfo.InvariantCulture))));

    /// <summary>
    /// Creates a new <see cref="DateTime"/> from an integer interpreted as Ticks (kind unspecified).
    /// </summary>
    protected static DateTime Convert(int value) => Convert(new DateTime(value));

    /// <summary>
    /// Creates a new <see cref="DateTime"/> from a float (after casting to integer Ticks, kind unspecified).
    /// </summary>
    protected static DateTime Convert(float value) => Convert(new DateTime((int)value));

    /// <summary>
    /// Creates a new <see cref="DateTime"/> from a decimal (after casting to integer Ticks, kind unspecified).
    /// </summary>
    protected static DateTime Convert(decimal value) => Convert(new DateTime((int)value));

    /// <summary>Implicitly converts the value object to its underlying <see cref="DateTime"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator DateTime(DateTimeValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated <see cref="DateTime"/> value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public override string ToString() => Value.ToString();
}
