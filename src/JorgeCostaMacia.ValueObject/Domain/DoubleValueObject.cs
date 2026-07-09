using System.Globalization;
using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="double"/> (double-precision floating-point) value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on double-precision numeric values.
/// It exposes the three-verb creation surface: the constructor hydrates (raw assignment for ORMs and
/// deserializers), <see cref="From(double)"/> converts (materializes, unvalidated) and
/// <see cref="Create(double)"/> fabricates validated (nothing invalid escapes it).
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="double"/>). The protected <c>Convert</c>
/// family carries the conversion logic from other primitive types, so Value Objects deriving from this one
/// in consuming contexts can reuse and redefine it.
/// </para>
/// </remarks>
public record DoubleValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="double"/> value encapsulated by this Value Object.
    /// </summary>
    public double Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The double value to encapsulate.</param>
    public DoubleValueObject(double value) => Value = value;

    /// <summary>
    /// Converts: materializes a new <see cref="DoubleValueObject"/> from the natural primitive through
    /// <see cref="Convert(double)"/>, <b>without validating it</b>. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source double value.</param>
    /// <returns>A new, unvalidated <see cref="DoubleValueObject"/> instance.</returns>
    public static DoubleValueObject From(double value) => new DoubleValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(double)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source double value.</param>
    /// <returns>A new, validated <see cref="DoubleValueObject"/> instance.</returns>
    /// <exception cref="DoubleValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static DoubleValueObject Create(double value)
    {
        DoubleValueObject vo = From(value);
        DoubleValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>Converts a double value (identity conversion).</summary>
    protected static double Convert(double value) => value;

    /// <summary>Parses a string into a double value, trimming whitespace first.</summary>
    protected static double Convert(string value) => Convert(double.Parse(value.Trim(), CultureInfo.InvariantCulture));

    /// <summary>Converts an integer to a double value.</summary>
    protected static double Convert(int value) => Convert((double)value);

    /// <summary>Converts a long to a double value.</summary>
    protected static double Convert(long value) => Convert((double)value);

    /// <summary>Converts a float to a double value.</summary>
    protected static double Convert(float value) => Convert((double)value);

    /// <summary>Converts a decimal to a double value (may lose precision).</summary>
    protected static double Convert(decimal value) => Convert((double)value);

    /// <summary>Converts a boolean to a double value (true = 1, false = 0).</summary>
    protected static double Convert(bool value) => Convert(value ? 1d : 0d);

    /// <summary>Implicitly converts the value object to its underlying <see cref="double"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator double(DoubleValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated double value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public override string ToString() => Value.ToString();
}
