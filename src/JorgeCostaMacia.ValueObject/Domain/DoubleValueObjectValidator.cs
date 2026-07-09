using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="DoubleValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator performs checks on the encapsulated <see cref="double"/> value.
/// Specific validation rules (e.g., range checks, non-negative constraints, minimum/maximum allowed values)
/// should be defined within the constructor.
/// </para>
/// </remarks>
public class DoubleValueObjectValidator : AbstractValidator<DoubleValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DoubleValueObjectValidator"/> class.
    /// Validation rules (if any) are defined within the body of this constructor.
    /// </summary>
    public DoubleValueObjectValidator() { }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator.
    /// This is the assembly the static <c>Create</c> factories of the Value Objects rely on.
    /// </summary>
    /// <returns>A new <see cref="DoubleValueObjectValidator"/> instance.</returns>
    public static DoubleValueObjectValidator Create() => new DoubleValueObjectValidator();

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
