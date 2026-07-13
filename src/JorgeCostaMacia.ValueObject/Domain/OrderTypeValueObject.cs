using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates an ordering direction ("ASC" or "DESC") for queries.
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="StringValueObject"/> and redefines the three-verb creation surface
/// for its own type: the constructor hydrates (ORMs, deserializers), <see cref="From(string)"/> converts
/// (normalizes through <see cref="Convert(string)"/>: trims and upper-cases, unvalidated) and
/// <see cref="Create(string)"/> fabricates validated.
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="string"/>). The protected <c>Convert</c>
/// family remains available for Value Objects deriving from this one in consuming contexts. The allowed
/// values are enforced by <see cref="OrderTypeValueObjectValidator"/>.
/// </para>
/// </remarks>
public record OrderTypeValueObject : StringValueObject
{
    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is through the base class constructor,
    /// bypassing normalization and validation. Reserved for infrastructure (ORMs, deserializers).
    /// </summary>
    /// <param name="value">The order-type string value to encapsulate.</param>
    public OrderTypeValueObject(string value) : base(value) { }

    /// <summary>
    /// Converts: normalizes the input through <see cref="Convert(string)"/> (trims whitespace and converts to
    /// uppercase) and materializes a new <see cref="OrderTypeValueObject"/>, <b>without validating it</b>.
    /// This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source order-type string value.</param>
    /// <returns>A new, normalized but unvalidated <see cref="OrderTypeValueObject"/> instance.</returns>
    public static new OrderTypeValueObject From(string value) => new OrderTypeValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(string)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source order-type string value.</param>
    /// <returns>A new, validated <see cref="OrderTypeValueObject"/> instance.</returns>
    /// <exception cref="OrderTypeValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static new OrderTypeValueObject Create(string value)
    {
        OrderTypeValueObject vo = From(value);
        OrderTypeValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }

    /// <summary>
    /// Converts the input string by first applying the base <see cref="StringValueObject.Convert(string)"/>
    /// cleansing (trimming) and then converting the result to uppercase.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The cleansed, uppercase string.</returns>
    protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
}
