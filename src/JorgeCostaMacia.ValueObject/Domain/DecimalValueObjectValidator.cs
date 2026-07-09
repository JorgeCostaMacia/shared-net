using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="DecimalValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator performs checks on the encapsulated <see cref="decimal"/> value.
/// Specific validation rules (e.g., range checks, non-negative constraints, minimum/maximum allowed values)
/// should be defined within the constructor.
/// </para>
/// </remarks>
public class DecimalValueObjectValidator : AbstractValidator<DecimalValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalValueObjectValidator"/> class.
    /// Validation rules (if any) are defined within the body of this constructor.
    /// </summary>
    public DecimalValueObjectValidator() { }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator.
    /// This is the assembly the static <c>Create</c> factories of the Value Objects rely on.
    /// </summary>
    /// <returns>A new <see cref="DecimalValueObjectValidator"/> instance.</returns>
    public static DecimalValueObjectValidator Create() => new DecimalValueObjectValidator();

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="DecimalValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="DecimalValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<DecimalValueObject> context, ValidationResult result)
        => throw new DecimalValueObjectValidationException(result.Errors);
}
