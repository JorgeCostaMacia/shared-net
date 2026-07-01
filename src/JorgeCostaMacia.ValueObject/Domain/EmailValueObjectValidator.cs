using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="EmailValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces constraints specific to valid email addresses.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Inherited Validation:</b> Includes all base validation rules defined in the injected <see cref="IValidator{T}"/> for the <see cref="StringValueObject"/>.
///     </description></item>
///     <item><description>
///         <b>Not Empty:</b> Ensures the email value is not empty or null.
///     </description></item>
///     <item><description>
///         <b>Format Check:</b> Applies the <c>EmailAddress</c> rule to validate the standard structure of the email address (e.g., presence of '@' and a domain).
///     </description></item>
/// </list>
/// </remarks>
public class EmailValueObjectValidator : AbstractValidator<EmailValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="StringValueObject"/> used to inherit base validation rules.</param>
    public EmailValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
            .NotEmpty()
            .EmailAddress();
    }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="EmailValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="EmailValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<EmailValueObject> context, ValidationResult result)
        => throw new EmailValueObjectValidationException(result.Errors);
}
