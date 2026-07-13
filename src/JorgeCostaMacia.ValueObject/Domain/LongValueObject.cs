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
/// family carries the conversion logic from other primitive types (often involving truncation), so Value
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
    /// Creates: materializes the value through <see cref="From(long)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source long value.</param>
    /// <returns>A new, validated <see cref="LongValueObject"/> instance.</returns>
    /// <exception cref="LongValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static LongValueObject Create(long value)
    {
        LongValueObject vo = From(value);
        LongValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>Converts a long value (identity conversion).</summary>
    protected static long Convert(long value) => value;

    /// <summary>Parses a string into a decimal, then truncates it to a long — the fractional part is dropped toward zero, never rounded (trimming whitespace first).</summary>
    protected static long Convert(string value) => Convert(decimal.Parse(value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture));

    /// <summary>Converts an integer to a long value (widening; exact).</summary>
    protected static long Convert(int value) => value;

    /// <summary>Converts a float to a long value, truncating the fractional part toward zero.</summary>
    protected static long Convert(float value) => Convert((decimal)value);

    /// <summary>Converts a double to a long value, truncating the fractional part toward zero.</summary>
    protected static long Convert(double value) => Convert((decimal)value);

    /// <summary>Converts a decimal to a long value, truncating the fractional part toward zero. Throws <see cref="OverflowException"/> if out of <see cref="long"/> range.</summary>
    protected static long Convert(decimal value) => (long)value;

    /// <summary>Converts a boolean to a long value (true = 1, false = 0).</summary>
    protected static long Convert(bool value) => value ? 1L : 0L;

    /// <summary>Implicitly converts the value object to its underlying <see cref="long"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator long(LongValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated long value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public override string ToString() => Value.ToString();
}
