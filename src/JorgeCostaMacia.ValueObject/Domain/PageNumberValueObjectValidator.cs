using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="PageNumberValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator ensures the page number is strictly positive (greater than zero), and <b>includes</b> the base
/// <see cref="IntValueObjectValidator"/> rules via constructor injection.
/// </para>
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
             .GreaterThan(0);
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="PageNumberValueObjectValidator"/> instance.</returns>
    public static PageNumberValueObjectValidator Create() => new PageNumberValueObjectValidator(IntValueObjectValidator.Create());

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
