using System.Globalization;
using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="int"/> (32-bit signed integer) value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on integer logic (e.g., ID, Count, Age).
/// It exposes the three-verb creation surface: the constructor hydrates (raw assignment for ORMs and
/// deserializers), <see cref="From(int)"/> converts (materializes, unvalidated) and
/// <see cref="Create(int)"/> fabricates validated (nothing invalid escapes it).
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="int"/>). The protected <c>Convert</c>
/// family carries the conversion logic from other primitive types (often involving truncation), so Value
/// Objects deriving from this one in consuming contexts can reuse and redefine it.
/// </para>
/// </remarks>
public record IntValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="int"/> value encapsulated by this Value Object.
    /// </summary>
    public int Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The integer value to encapsulate.</param>
    public IntValueObject(int value) => Value = value;

    /// <summary>
    /// Converts: materializes a new <see cref="IntValueObject"/> from the natural primitive through
    /// <see cref="Convert(int)"/>, <b>without validating it</b>. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source integer value.</param>
    /// <returns>A new, unvalidated <see cref="IntValueObject"/> instance.</returns>
    public static IntValueObject From(int value) => new IntValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(int)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source integer value.</param>
    /// <returns>A new, validated <see cref="IntValueObject"/> instance.</returns>
    /// <exception cref="IntValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static IntValueObject Create(int value)
    {
        IntValueObject vo = From(value);
        IntValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Converts an integer value (identity conversion).
    /// </summary>
    protected static int Convert(int value) => value;

    /// <summary>
    /// Parses a string into a decimal, then truncates it to an integer — the fractional part is dropped (toward zero), never rounded (trimming whitespace first).
    /// </summary>
    protected static int Convert(string value) => Convert(decimal.Parse(value.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts a float to an integer, truncating the fractional part toward zero (e.g. 5.9 and -5.9 become 5 and -5).
    /// </summary>
    protected static int Convert(float value) => Convert((decimal)value);

    /// <summary>
    /// Converts a decimal to an integer, truncating the fractional part toward zero. Throws <see cref="OverflowException"/> if the value is out of <see cref="int"/> range.
    /// </summary>
    protected static int Convert(decimal value) => (int)value;

    /// <summary>
    /// Converts a boolean to an integer (1 for true, 0 for false).
    /// </summary>
    protected static int Convert(bool value) => value ? 1 : 0;

    /// <summary>
    /// Converts a long to an integer. Throws <see cref="OverflowException"/> if the value is out of <see cref="int"/> range.
    /// </summary>
    protected static int Convert(long value) => Convert((decimal)value);

    /// <summary>
    /// Converts a double to an integer, truncating the fractional part toward zero.
    /// </summary>
    protected static int Convert(double value) => Convert((decimal)value);

    /// <summary>Implicitly converts the value object to its underlying <see cref="int"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator int(IntValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated integer value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public override string ToString() => Value.ToString();
}
