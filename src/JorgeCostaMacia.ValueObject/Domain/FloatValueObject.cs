using System.Globalization;
using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="float"/> (single-precision floating-point) value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on numeric values that require floating-point arithmetic (e.g., coordinates, physical measurements).
/// It exposes the three-verb creation surface: the constructor hydrates (raw assignment for ORMs and
/// deserializers), <see cref="From(float)"/> converts (materializes, unvalidated) and
/// <see cref="Create(float)"/> fabricates validated (nothing invalid escapes it).
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="float"/>). The protected <c>Convert</c>
/// family carries the conversion logic from other primitive types, so Value Objects deriving from this one
/// in consuming contexts can reuse and redefine it.
/// </para>
/// </remarks>
public record FloatValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="float"/> value encapsulated by this Value Object.
    /// </summary>
    public float Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The float value to encapsulate.</param>
    public FloatValueObject(float value) => Value = value;

    /// <summary>
    /// Converts: materializes a new <see cref="FloatValueObject"/> from the natural primitive through
    /// <see cref="Convert(float)"/>, <b>without validating it</b>. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source float value.</param>
    /// <returns>A new, unvalidated <see cref="FloatValueObject"/> instance.</returns>
    public static FloatValueObject From(float value) => new FloatValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(float)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source float value.</param>
    /// <returns>A new, validated <see cref="FloatValueObject"/> instance.</returns>
    /// <exception cref="FloatValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static FloatValueObject Create(float value)
    {
        FloatValueObject vo = From(value);
        FloatValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Converts a float value (identity conversion).
    /// </summary>
    protected static float Convert(float value) => value;

    /// <summary>
    /// Parses a string into a float value, trimming whitespace first.
    /// </summary>
    protected static float Convert(string value) => Convert(float.Parse(value.Trim(), CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts an integer to a float value.
    /// </summary>
    protected static float Convert(int value) => Convert((float)value);

    /// <summary>
    /// Converts a decimal to a float value.
    /// </summary>
    protected static float Convert(decimal value) => Convert((float)value);

    /// <summary>
    /// Converts a boolean to a float value (true = 1, false = 0).
    /// </summary>
    protected static float Convert(bool value) => Convert(value ? 1 : 0);

    /// <summary>
    /// Converts a long to a float value.
    /// </summary>
    protected static float Convert(long value) => Convert((float)value);

    /// <summary>
    /// Converts a double to a float value (may lose precision).
    /// </summary>
    protected static float Convert(double value) => Convert((float)value);

    /// <summary>Implicitly converts the value object to its underlying <see cref="float"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator float(FloatValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated float value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public override string ToString() => Value.ToString();
}
