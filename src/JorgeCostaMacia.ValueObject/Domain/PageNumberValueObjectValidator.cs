using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="PageNumberValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator ensures the page number value meets the criteria for a valid page in a document or data set.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Inherited Validation:</b> Includes all base integer validation rules defined in the injected <see cref="IValidator{T}"/> for the <see cref="IntValueObject"/> (e.g., numeric format validity).
///     </description></item>
///     <item><description>
///         <b>Not Empty:</b> Ensures the page number value is set.
///     </description></item>
///     <item><description>
///         <b>Domain Constraint (Implicit/Assumed):</b> While not explicitly visible in this code, validation often ensures the value is greater than zero (since page numbers start at 1).
///     </description></item>
/// </list>
/// </remarks>
public class PageNumberValueObjectValidator : AbstractValidator<PageNumberValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PageNumberValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="IntValueObject"/> used to inherit base validation rules.</param>
    public PageNumberValueObjectValidator(IValidator<IntValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
             .NotEmpty();
    }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="PageNumberValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="PageNumberValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<PageNumberValueObject> context, ValidationResult result)
        => throw new PageNumberValueObjectValidationException(result.Errors);
}
