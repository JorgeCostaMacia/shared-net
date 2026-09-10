using System.Globalization;
using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a single <see cref="decimal"/> value.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the base for domain Value Objects based on precise numeric values (e.g., Money, Quantity, Price).
/// It exposes the three-verb creation surface: the constructor hydrates (raw assignment for ORMs and
/// deserializers), <see cref="From(decimal)"/> converts (materializes, unvalidated) and
/// <see cref="Create(decimal)"/> fabricates validated (nothing invalid escapes it).
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="decimal"/>). The protected <c>Convert</c>
/// family carries the conversion logic from other primitive types, so Value Objects deriving from this one
/// in consuming contexts can reuse and redefine it.
/// </para>
/// </remarks>
public record DecimalValueObject : IValueObject
{
    /// <summary>
    /// The immutable <see cref="decimal"/> value encapsulated by this Value Object.
    /// </summary>
    public decimal Value { get; init; }

    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is, bypassing validation.
    /// Reserved for infrastructure (ORMs, deserializers, database mapping — the EF converters rely on it).
    /// </summary>
    /// <param name="value">The decimal value to encapsulate.</param>
    public DecimalValueObject(decimal value)
    {
        Value = value;
    }

    /// <summary>
    /// Converts: materializes a new <see cref="DecimalValueObject"/> from the natural primitive through
    /// <see cref="Convert(decimal)"/>, <b>without validating it</b>. This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source decimal value.</param>
    /// <returns>A new, unvalidated <see cref="DecimalValueObject"/> instance.</returns>
    public static DecimalValueObject From(decimal value) => new DecimalValueObject(Convert(value));

    /// <summary>
    /// Converts, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise
    /// the same result as <see cref="From(decimal)"/> — <b>without validating it</b>.
    /// </summary>
    /// <param name="value">The decimal value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, unvalidated <see cref="DecimalValueObject"/> instance, or <see langword="null"/>.</returns>
    public static DecimalValueObject? FromOrNull(decimal? value) => value is null ? null : From(value.Value);

    /// <summary>
    /// Creates: materializes the value through <see cref="From(decimal)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source decimal value.</param>
    /// <returns>A new, validated <see cref="DecimalValueObject"/> instance.</returns>
    /// <exception cref="DecimalValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static DecimalValueObject Create(decimal value)
    {
        DecimalValueObject vo = From(value);
        vo.Validate();

        return vo;
    }

    /// <summary>
    /// Creates, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise the
    /// same result as <see cref="Create(decimal)"/>. Absence short-circuits; a supplied value still has to be valid.
    /// </summary>
    /// <param name="value">The decimal value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, validated <see cref="DecimalValueObject"/> instance, or <see langword="null"/>.</returns>
    /// <exception cref="DecimalValueObjectValidationException">Thrown when a supplied value violates a validation rule.</exception>
    public static DecimalValueObject? CreateOrNull(decimal? value)
    {
        DecimalValueObject? vo = FromOrNull(value);
        vo?.Validate();

        return vo;
    }

    /// <summary>Runs this value object through its own validator, throwing when a rule fails.</summary>
    private void Validate() => DecimalValueObjectValidator.Create().ValidateAndThrow(this);

    /// <summary>
    /// Converts a decimal value (identity conversion).
    /// </summary>
    protected static decimal Convert(decimal value) => value;

    /// <summary>
    /// Parses a string into a decimal value, trimming whitespace first.
    /// </summary>
    protected static decimal Convert(string value) => Convert(decimal.Parse(value.Trim(), CultureInfo.InvariantCulture));

    /// <summary>
    /// Converts an integer to a decimal value.
    /// </summary>
    protected static decimal Convert(int value) => Convert((decimal)value);

    /// <summary>
    /// Converts a float to a decimal value.
    /// </summary>
    protected static decimal Convert(float value) => Convert(System.Convert.ToDecimal(value));

    /// <summary>
    /// Converts a boolean to a decimal value (true = 1, false = 0).
    /// </summary>
    protected static decimal Convert(bool value) => Convert(value ? 1 : 0);

    /// <summary>
    /// Converts a long to a decimal value.
    /// </summary>
    protected static decimal Convert(long value) => Convert((decimal)value);

    /// <summary>
    /// Converts a double to a decimal value (may involve overflow).
    /// </summary>
    protected static decimal Convert(double value) => Convert(System.Convert.ToDecimal(value));

    /// <summary>Implicitly converts the value object to its underlying <see cref="decimal"/> value.</summary>
    /// <param name="valueObject">The value object to convert.</param>
    public static implicit operator decimal(DecimalValueObject valueObject) => valueObject.Value;

    /// <summary>
    /// Returns the string representation of the encapsulated decimal value.
    /// </summary>
    /// <returns>The internal value (<see cref="Value"/>) as a string.</returns>
    public sealed override string ToString() => Value.ToString();
}
