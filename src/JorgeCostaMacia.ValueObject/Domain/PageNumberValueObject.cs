using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a page number for pagination (a positive integer).
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="IntValueObject"/> and redefines the three-verb creation surface
/// for its own type: the constructor hydrates (ORMs, deserializers), <see cref="From(int)"/> converts
/// (materializes, unvalidated) and <see cref="Create(int)"/> fabricates validated.
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="int"/>) and convert through the inherited
/// <c>Convert</c> family. The positivity constraint is enforced by <see cref="PageNumberValueObjectValidator"/>.
/// </para>
/// </remarks>
public record PageNumberValueObject : IntValueObject
{
    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is through the base class constructor,
    /// bypassing validation. Reserved for infrastructure (ORMs, deserializers).
    /// </summary>
    /// <param name="value">The page number value to encapsulate.</param>
    public PageNumberValueObject(int value) : base(value) { }

    /// <summary>
    /// Converts: materializes a new <see cref="PageNumberValueObject"/> from the natural primitive through
    /// the base <see cref="IntValueObject.Convert(int)"/>, <b>without validating it</b>.
    /// This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source page number value.</param>
    /// <returns>A new, unvalidated <see cref="PageNumberValueObject"/> instance.</returns>
    public new static PageNumberValueObject From(int value) => new PageNumberValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(int)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source page number value.</param>
    /// <returns>A new, validated <see cref="PageNumberValueObject"/> instance.</returns>
    /// <exception cref="PageNumberValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public new static PageNumberValueObject Create(int value)
    {
        PageNumberValueObject vo = From(value);
        PageNumberValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }
}
