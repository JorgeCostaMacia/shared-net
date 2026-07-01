using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="PageNumberRangeValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces consistency within the page number range, ensuring that the start value
/// is not numerically greater than the end value.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Nested Validation:</b> Applies an injected <see cref="IValidator{T}"/> to individually validate
///         both the <see cref="PageNumberRangeValueObject.ValueStart"/> and <see cref="PageNumberRangeValueObject.ValueEnd"/> properties,
///         ensuring both are valid page numbers (e.g., greater than zero).
///     </description></item>
///     <item><description>
///         <b>Range Invariant:</b> Contains rules to guarantee that the start page number is less than or equal to the end page number.
///     </description></item>
/// </list>
/// </remarks>
public class PageNumberRangeValueObjectValidator : AbstractValidator<PageNumberRangeValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PageNumberRangeValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="PageNumberValueObject"/> used to validate the range's individual components (start and end page numbers).</param>
    public PageNumberRangeValueObjectValidator(IValidator<PageNumberValueObject> validator)
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
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="PageNumberRangeValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="PageNumberRangeValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<PageNumberRangeValueObject> context, ValidationResult result)
        => throw new PageNumberRangeValueObjectValidationException(result.Errors);
}
