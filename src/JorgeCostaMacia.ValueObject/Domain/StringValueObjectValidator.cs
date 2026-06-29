using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="StringValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator serves as the <b>base validator</b> for all domain Value Objects that encapsulate a string.
/// Specific validation rules (e.g., length limits, allowed characters, format checks) should be defined
/// within the constructor, or in derived validators that include this one via <c>Include(validator)</c>.
/// </para>
/// <para>
/// Because this is the base implementation, it currently contains no explicit validation rules,
/// relying on derived classes to add constraints like <c>NotEmpty</c> or <c>MaximumLength</c>.
/// </para>
/// </remarks>
public class StringValueObjectValidator : AbstractValidator<StringValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringValueObjectValidator"/> class.
    /// Validation rules (if any) should be defined here, although it typically remains empty to act as a base.
    /// </summary>
    public StringValueObjectValidator() { }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="StringValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="StringValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<StringValueObject> context, ValidationResult result)
        => throw new StringValueObjectValidationException(null, null, null, null, null, null, null, result.Errors);
}