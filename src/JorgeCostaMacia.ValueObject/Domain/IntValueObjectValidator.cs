using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="IntValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator performs checks on the encapsulated <see cref="int"/> value.
/// Specific validation rules (e.g., range checks, non-negative constraints, minimum/maximum allowed values)
/// should be defined within the constructor.
/// </para>
/// </remarks>
public class IntValueObjectValidator : AbstractValidator<IntValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntValueObjectValidator"/> class.
    /// Validation rules (if any) are defined within the body of this constructor.
    /// </summary>
    public IntValueObjectValidator() { }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="IntValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="IntValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<IntValueObject> context, ValidationResult result)
        => throw new IntValueObjectValidationException(result.Errors);
}