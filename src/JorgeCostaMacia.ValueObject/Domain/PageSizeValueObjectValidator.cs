using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="PageSizeValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator ensures the page size is strictly positive (greater than zero), and <b>includes</b> the base
/// <see cref="IntValueObjectValidator"/> rules via constructor injection.
/// </para>
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
             .GreaterThan(0);
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="PageSizeValueObjectValidator"/> instance.</returns>
    public static PageSizeValueObjectValidator Create() => new PageSizeValueObjectValidator(IntValueObjectValidator.Create());

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
