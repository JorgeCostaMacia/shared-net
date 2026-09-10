using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a JSON document as a string.
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="StringValueObject"/> and redefines the three-verb creation surface
/// for its own type: the constructor hydrates (ORMs, deserializers), <see cref="From(string)"/> converts
/// (normalizes by trimming, unvalidated) and <see cref="Create(string)"/> fabricates validated.
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="string"/>) and normalize through the
/// inherited <c>Convert</c> family. The validation of the JSON structure itself is handled by
/// <see cref="JsonValueObjectValidator"/>.
/// </para>
/// </remarks>
public record JsonValueObject : StringValueObject
{
    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is through the base class constructor,
    /// bypassing normalization and validation. Reserved for infrastructure (ORMs, deserializers).
    /// </summary>
    /// <param name="value">The JSON string value to encapsulate.</param>
    public JsonValueObject(string value) : base(value) { }

    /// <summary>
    /// Converts: normalizes the input through the base <see cref="StringValueObject.Convert(string)"/> (trims
    /// whitespace) and materializes a new <see cref="JsonValueObject"/>, <b>without validating it</b>.
    /// This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source JSON string value.</param>
    /// <returns>A new, normalized but unvalidated <see cref="JsonValueObject"/> instance.</returns>
    public static new JsonValueObject From(string value) => new JsonValueObject(Convert(value));

    /// <summary>
    /// Converts, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise
    /// the same result as <see cref="From(string)"/> — <b>without validating it</b>.
    /// </summary>
    /// <param name="value">The JSON string value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, unvalidated <see cref="JsonValueObject"/> instance, or <see langword="null"/>.</returns>
    public static new JsonValueObject? FromOrNull(string? value) => value is null ? null : From(value);

    /// <summary>
    /// Creates: materializes the value through <see cref="From(string)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source JSON string value.</param>
    /// <returns>A new, validated <see cref="JsonValueObject"/> instance.</returns>
    /// <exception cref="JsonValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static new JsonValueObject Create(string value)
    {
        JsonValueObject vo = From(value);
        vo.Validate();

        return vo;
    }

    /// <summary>
    /// Creates, propagating absence: gives <see langword="null"/> when <paramref name="value"/> is <see langword="null"/>, otherwise the
    /// same result as <see cref="Create(string)"/>. Absence short-circuits; a supplied value still has to be valid.
    /// </summary>
    /// <param name="value">The JSON string value to encapsulate, or <see langword="null"/>.</param>
    /// <returns>A new, validated <see cref="JsonValueObject"/> instance, or <see langword="null"/>.</returns>
    /// <exception cref="JsonValueObjectValidationException">Thrown when a supplied value violates a validation rule.</exception>
    public static new JsonValueObject? CreateOrNull(string? value)
    {
        JsonValueObject? vo = FromOrNull(value);
        vo?.Validate();

        return vo;
    }

    /// <summary>Runs this value object through its own validator, throwing when a rule fails.</summary>
    private void Validate() => JsonValueObjectValidator.Create().ValidateAndThrow(this);
}
