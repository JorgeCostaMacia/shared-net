using System.Globalization;
using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="long"/> (64-bit signed integer) value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on 64-bit integer logic (e.g., large IDs, counters).
/// It exposes the three-verb creation surface: the constructor hydrates (raw assignment for ORMs and
/// deserializers), <see cref="From(long)"/> converts (materializes, unvalidated) and
/// <see cref="Create(long)"/> fabricates validated (nothing invalid escapes it).
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="long"/>). The protected <c>Convert</c>
/// family carries the conversion logic from other primitive types (a fractional part is rejected, never truncated), so Value
/// Objects deriving from this one in consuming contexts can reuse and redefine it.
/// </para>
/// </remarks>
public record LongValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="long"/> value encapsulated by this Value Object.
    /// </summary>
    public long Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The long value to encapsulate.</param>
    public LongValueObject(long value)
    {
        Value = value;
    }

    /// <summary>
    /// Converts: materializes a new <see cref="LongValueObject"/> from the natural primitive through
    /// <see cref="Convert(long)"/>, <b>without validating it</b>. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source long value.</param>
    /// <returns>A new, unvalidated <see cref="LongValueObject"/> instance.</returns>
    public static LongValueObject From(long value) => new LongValueObject(Convert(value));

    /// <summary>
    /// Converts, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise
    /// the same result as <see cref="From(long)"/> — <b>without validating it</b>.
    /// </summary>
    /// <param name="value">The long value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, unvalidated <see cref="LongValueObject"/> instance, or <see langword="null"/>.</returns>
    public static LongValueObject? FromOrNull(long? value) => value is null ? null : From(value.Value);

    /// <summary>
    /// Creates: materializes the value through <see cref="From(long)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source long value.</param>
    /// <returns>A new, validated <see cref="LongValueObject"/> instance.</returns>
    /// <exception cref="LongValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static LongValueObject Create(long value)
    {
        LongValueObject vo = From(value);
        vo.Validate();

        return vo;
    }

    /// <summary>
    /// Creates, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise the
    /// same result as <see cref="Create(long)"/>. Absence short-circuits; a supplied value still has to be valid.
    /// </summary>
    /// <param name="value">The long value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, validated <see cref="LongValueObject"/> instance, or <see langword="null"/>.</returns>
    /// <exception cref="LongValueObjectValidationException">Thrown when a supplied value violates a validation rule.</exception>
    public static LongValueObject? CreateOrNull(long? value)
    {
        LongValueObject? vo = FromOrNull(value);
        vo?.Validate();

        return vo;
    }

    /// <summary>Runs this value object through its own validator, throwing when a rule fails.</summary>
    private void Validate() => LongValueObjectValidator.Create().ValidateAndThrow(this);

    /// <summary>Converts a long value (identity conversion).</summary>
    protected static long Convert(long value) => value;

    /// <summary>
    /// Parses a string into a decimal (trimming whitespace first), then converts it to a long: <c>"5.0"</c> is accepted,
    /// <c>"5.9"</c> throws <see cref="FormatException"/>.
    /// </summary>
    protected static long Convert(string value) => Convert(decimal.Parse(value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture));

    /// <summary>Converts an integer to a long value (widening; exact).</summary>
    protected static long Convert(int value) => value;

    /// <summary>
    /// Converts a float to a long value. Throws <see cref="FormatException"/> if it has a fractional part (or is NaN) and
    /// <see cref="OverflowException"/> if it is out of <see cref="long"/> range. Checked on the float itself, not through
    /// decimal, which keeps only 7 significant digits.
    /// </summary>
    protected static long Convert(float value)
        => value == float.Truncate(value) ? checked((long)value) : throw new FormatException($"{value} is not an integer");

    /// <summary>
    /// Converts a double to a long value. Throws <see cref="FormatException"/> if it has a fractional part (or is NaN) and
    /// <see cref="OverflowException"/> if it is out of <see cref="long"/> range. Checked on the double itself, not through
    /// decimal, which keeps only 15 significant digits: 1234567890123456768d would come out as 1234567890123460000.
    /// </summary>
    protected static long Convert(double value)
        => value == double.Truncate(value) ? checked((long)value) : throw new FormatException($"{value} is not an integer");

    /// <summary>
    /// Converts a decimal to a long value. A fractional part throws <see cref="FormatException"/> instead of being dropped:
    /// truncating 5.9 to 5 would invent the value, and a field that wants to truncate or round says so in its own
    /// Value Object. Throws <see cref="OverflowException"/> if out of <see cref="long"/> range.
    /// </summary>
    protected static long Convert(decimal value)
        => value == decimal.Truncate(value) ? (long)value : throw new FormatException($"{value} is not an integer");

    /// <summary>Converts a boolean to a long value (true = 1, false = 0).</summary>
    protected static long Convert(bool value) => value ? 1L : 0L;

    /// <summary>Implicitly converts the value object to its underlying <see cref="long"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator long(LongValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated long value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public sealed override string ToString() => Value.ToString();
}
