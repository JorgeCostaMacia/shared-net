using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="FloatValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator performs checks on the encapsulated <see cref="float"/> value.
/// Specific validation rules (e.g., range checks, positive/negative constraints, precision checks)
/// should be defined within the constructor.
/// </para>
/// </remarks>
public class FloatValueObjectValidator : AbstractValidator<FloatValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FloatValueObjectValidator"/> class.
    /// Validation rules (if any) should be defined here, although it typically remains empty to act as a base.
    /// </summary>
    public FloatValueObjectValidator() { }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="FloatValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="FloatValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<FloatValueObject> context, ValidationResult result)
        => throw new FloatValueObjectValidationException(result.Errors);
}