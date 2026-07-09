using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="IntRangeValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces consistency within the integer range, ensuring that the start value
/// is not numerically greater than the end value.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Nested Validation:</b> Applies an injected <see cref="IValidator{T}"/> to individually validate
///         both the <see cref="IntRangeValueObject.ValueStart"/> and <see cref="IntRangeValueObject.ValueEnd"/> properties.
///     </description></item>
///     <item><description>
///         <b>Range Invariant:</b> Contains rules to guarantee that the start value is less than or equal to the end value.
///     </description></item>
/// </list>
/// </remarks>
public class IntRangeValueObjectValidator : AbstractValidator<IntRangeValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntRangeValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="IntValueObject"/> used to validate the range's individual components (start and end values).</param>
    public IntRangeValueObjectValidator(IValidator<IntValueObject> validator)
    {
        RuleFor(v => v.ValueStart)
             .SetValidator(validator);

        RuleFor(v => v.ValueEnd)
            .SetValidator(validator);

        RuleFor(v => v.ValueStart.Value)
            .LessThanOrEqualTo(v => v.ValueEnd.Value);

        RuleFor(v => v.ValueEnd.Value)
            .GreaterThanOrEqualTo(v => v.ValueStart.Value);
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="IntRangeValueObjectValidator"/> instance.</returns>
    public static IntRangeValueObjectValidator Create() => new IntRangeValueObjectValidator(IntValueObjectValidator.Create());

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="IntRangeValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="IntRangeValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<IntRangeValueObject> context, ValidationResult result)
        => throw new IntRangeValueObjectValidationException(result.Errors);
}
