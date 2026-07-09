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
    public new static JsonValueObject From(string value) => new JsonValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(string)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source JSON string value.</param>
    /// <returns>A new, validated <see cref="JsonValueObject"/> instance.</returns>
    /// <exception cref="JsonValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public new static JsonValueObject Create(string value)
    {
        JsonValueObject vo = From(value);
        JsonValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }
}
