using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a valid email address.
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="StringValueObject"/> and redefines the three-verb creation surface
/// for its own type: the constructor hydrates (ORMs, deserializers), <see cref="From(string)"/> converts
/// (normalizes by trimming, unvalidated) and <see cref="Create(string)"/> fabricates validated.
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="string"/>) and normalize through the
/// inherited <c>Convert</c> family. The validation of the email format itself is handled by
/// <see cref="EmailValueObjectValidator"/>.
/// </para>
/// </remarks>
public record EmailValueObject : StringValueObject
{
    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is through the base class constructor,
    /// bypassing normalization and validation. Reserved for infrastructure (ORMs, deserializers).
    /// </summary>
    /// <param name="value">The email string value to encapsulate.</param>
    public EmailValueObject(string value) : base(value) { }

    /// <summary>
    /// Converts: normalizes the input through the base <see cref="StringValueObject.Convert(string)"/> (trims
    /// whitespace) and materializes a new <see cref="EmailValueObject"/>, <b>without validating it</b>.
    /// This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source email string value.</param>
    /// <returns>A new, normalized but unvalidated <see cref="EmailValueObject"/> instance.</returns>
    public new static EmailValueObject From(string value) => new EmailValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(string)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source email string value.</param>
    /// <returns>A new, validated <see cref="EmailValueObject"/> instance.</returns>
    /// <exception cref="EmailValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public new static EmailValueObject Create(string value)
    {
        EmailValueObject vo = From(value);
        EmailValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }
}
