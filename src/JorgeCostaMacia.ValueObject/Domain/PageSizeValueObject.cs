using FluentValidation;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents an immutable <b>Value Object</b> that encapsulates a page size for pagination (a positive integer).
/// </summary>
/// <remarks>
/// <para>
/// This class inherits from <see cref="IntValueObject"/> and redefines the three-verb creation surface
/// for its own type: the constructor hydrates (ORMs, deserializers), <see cref="From(int)"/> converts
/// (materializes, unvalidated) and <see cref="Create(int)"/> fabricates validated.
/// </para>
/// <para>
/// Both factories take the type's natural primitive (<see cref="int"/>) and convert through the inherited
/// <c>Convert</c> family. The positivity constraint is enforced by <see cref="PageSizeValueObjectValidator"/>.
/// </para>
/// </remarks>
public record PageSizeValueObject : IntValueObject
{
    /// <summary>
    /// <b>Hydration Constructor.</b> Assigns the value as-is through the base class constructor,
    /// bypassing validation. Reserved for infrastructure (ORMs, deserializers).
    /// </summary>
    /// <param name="value">The page size value to encapsulate.</param>
    public PageSizeValueObject(int value) : base(value) { }

    /// <summary>
    /// Converts: materializes a new <see cref="PageSizeValueObject"/> from the natural primitive through
    /// the base <see cref="IntValueObject.Convert(int)"/>, <b>without validating it</b>.
    /// This is the path composites use to build their parts.
    /// </summary>
    /// <param name="value">The source page size value.</param>
    /// <returns>A new, unvalidated <see cref="PageSizeValueObject"/> instance.</returns>
    public static new PageSizeValueObject From(int value) => new PageSizeValueObject(Convert(value));

    /// <summary>
    /// Creates: materializes the value through <see cref="From(int)"/> and validates it —
    /// nothing invalid escapes this factory.
    /// </summary>
    /// <param name="value">The source page size value.</param>
    /// <returns>A new, validated <see cref="PageSizeValueObject"/> instance.</returns>
    /// <exception cref="PageSizeValueObjectValidationException">Thrown when the resulting value violates a validation rule.</exception>
    public static new PageSizeValueObject Create(int value)
    {
        PageSizeValueObject vo = From(value);
        PageSizeValueObjectValidator.Create().ValidateAndThrow(vo);

        return vo;
    }
}
