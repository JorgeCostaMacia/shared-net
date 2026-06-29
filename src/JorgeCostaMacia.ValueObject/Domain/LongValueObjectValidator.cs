using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="LongValueObject"/>.
/// </summary>
/// <remarks>
/// Validation rules (if any) are defined within the body of the constructor.
/// </remarks>
public class LongValueObjectValidator : AbstractValidator<LongValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LongValueObjectValidator"/> class.
    /// </summary>
    public LongValueObjectValidator() { }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="LongValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="LongValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<LongValueObject> context, ValidationResult result)
        => throw new LongValueObjectValidationException(result.Errors);
}
