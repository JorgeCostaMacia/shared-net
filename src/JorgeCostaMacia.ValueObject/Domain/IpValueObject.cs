using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates an IP address string (either IPv4 or IPv6).
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="StringValueObject"/> and redefines the three-verb creation surface
/// for its own type: the constructor hydrates (ORMs, deserializers), <see cref="From(string)"/> converts
/// (normalizes by trimming, unvalidated) and <see cref="Create(string)"/> fabricates validated.
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="string"/>) and normalize through the
/// inherited <c>Convert</c> family. The validation of the IP format itself is handled by
/// <see cref="IpValueObjectValidator"/>.
/// </para>
/// </remarks>
public record IpValueObject : StringValueObject
{
    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is through the base class constructor,
    /// bypassing normalization and validation. Reserved for infrastructure (ORMs, deserializers).
    /// </summary>
    /// <param name="value">The IP address string value to encapsulate.</param>
    public IpValueObject(string value) : base(value) { }

    /// <summary>
    /// Converts: normalizes the input through the base <see cref="StringValueObject.Convert(string)"/> (trims
    /// whitespace) and materializes a new <see cref="IpValueObject"/>, <b>without validating it</b>.
    /// This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source IP address string value.</param>
    /// <returns>A new, normalized but unvalidated <see cref="IpValueObject"/> instance.</returns>
    public static new IpValueObject From(string value) => new IpValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(string)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source IP address string value.</param>
    /// <returns>A new, validated <see cref="IpValueObject"/> instance.</returns>
    /// <exception cref="IpValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static new IpValueObject Create(string value)
    {
        IpValueObject vo = From(value);
        IpValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }
}
