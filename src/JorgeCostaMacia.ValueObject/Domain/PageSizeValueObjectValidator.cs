using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="PageSizeValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator ensures the page size value meets the criteria for a valid size in a pagination context.
/// Domain-specific rules, such as maximum allowed page size, are typically defined here.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Inherited Validation:</b> Includes all base integer validation rules defined in the injected <see cref="IValidator{T}"/> for the <see cref="IntValueObject"/> (e.g., numeric format validity).
///     </description></item>
///     <item><description>
///         <b>Not Empty:</b> Ensures the page size value is set.
///     </description></item>
///     <item><description>
///         <b>Domain Constraint (Assumed):</b> Additional rules (e.g., <c>.GreaterThan(0)</c> or <c>.LessThanOrEqualTo(MAX_LIMIT)</c>) are expected here to enforce page size constraints.
///     </description></item>
/// </list>
/// </remarks>
public class PageSizeValueObjectValidator : AbstractValidator<PageSizeValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PageSizeValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="IntValueObject"/> used to inherit base validation rules.</param>
    public PageSizeValueObjectValidator(IValidator<IntValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
             .NotEmpty();
    }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="PageSizeValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="PageSizeValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<PageSizeValueObject> context, ValidationResult result)
        => throw new PageSizeValueObjectValidationException(result.Errors);
}