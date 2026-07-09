using System.Globalization;
using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable, generic <b>Value Object</b> that encapsulates a single <see cref="string"/> value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for all domain Value Objects based on text strings (e.g., Email, ClientName).
/// It exposes the three-verb creation surface: the constructor hydrates (raw assignment for ORMs and
/// deserializers), <see cref="From(string)"/> converts (normalizes and materializes, unvalidated) and
/// <see cref="Create(string)"/> fabricates validated (nothing invalid escapes it).
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="string"/>). The protected <c>Convert</c>
/// family carries the conversion/cleansing logic from other primitive types, so Value Objects deriving
/// from this one in consuming contexts can reuse and redefine it.
/// </para>
/// </remarks>
public record StringValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="string"/> value encapsulated by this Value Object.
    /// </summary>
    public string Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing normalization and validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The string value to encapsulate.</param>
    public StringValueObject(string value) => Value = value;

    /// <summary>
    /// Converts: normalizes the input through <see cref="Convert(string)"/> (trims whitespace) and materializes
    /// a new <see cref="StringValueObject"/>, <b>without validating it</b>. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source string value.</param>
    /// <returns>A new, normalized but unvalidated <see cref="StringValueObject"/> instance.</returns>
    public static StringValueObject From(string value) => new StringValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(string)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source string value.</param>
    /// <returns>A new, validated <see cref="StringValueObject"/> instance.</returns>
    /// <exception cref="StringValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static StringValueObject Create(string value)
    {
        StringValueObject vo = From(value);
        StringValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Converts and cleanses a string value. By default, this method trims whitespace.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The cleansed string.</returns>
    protected static string Convert(string value) => value.Trim();

    /// <summary>
    /// Converts an integer to a string.
    /// </summary>
    protected static string Convert(int value) => Convert(value.ToString());

    /// <summary>
    /// Converts a float to a string.
    /// </summary>
    protected static string Convert(float value) => Convert(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts a decimal to a string.
    /// </summary>
    protected static string Convert(decimal value) => Convert(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts a boolean to a string.
    /// </summary>
    protected static string Convert(bool value) => Convert(value.ToString());

    /// <summary>
    /// Converts a long to a string.
    /// </summary>
    protected static string Convert(long value) => Convert(value.ToString());

    /// <summary>
    /// Converts a double to a string.
    /// </summary>
    protected static string Convert(double value) => Convert(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts a DateTime to a string.
    /// </summary>
    protected static string Convert(DateTime value) => Convert(value.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts a Guid to a string.
    /// </summary>
    protected static string Convert(Guid value) => Convert(value.ToString());

    /// <summary>Implicitly converts the value object to its underlying <see cref="string"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator string(StringValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>).</returns>
    public override string ToString() => Value.ToString();
}
