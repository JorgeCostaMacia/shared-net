using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="DoubleValueObject"/>.
/// </summary>
/// <remarks>
/// Validation rules (if any) are defined within the body of the constructor.
/// </remarks>
public class DoubleValueObjectValidator : AbstractValidator<DoubleValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoubleValueObjectValidator"/> class.
    /// </summary>
    public DoubleValueObjectValidator() { }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="DoubleValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="DoubleValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<DoubleValueObject> context, ValidationResult result)
        => throw new DoubleValueObjectValidationException(result.Errors);
}
