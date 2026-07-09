using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="BoolValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator performs checks on the encapsulated <see cref="bool"/> value.
/// Specific validation rules (if any) should be defined within the constructor.
/// </para>
/// </remarks>
public class BoolValueObjectValidator : AbstractValidator<BoolValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BoolValueObjectValidator"/> class.
    /// Validation rules (if any) are defined within the body of this constructor.
    /// </summary>
    public BoolValueObjectValidator() { }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator.
    /// This is the assembly the static <c>Create</c> factories of the Value Objects rely on.
    /// </summary>
    /// <returns>A new <see cref="BoolValueObjectValidator"/> instance.</returns>
    public static BoolValueObjectValidator Create() => new BoolValueObjectValidator();

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="BoolValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="BoolValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<BoolValueObject> context, ValidationResult result)
        => throw new BoolValueObjectValidationException(result.Errors);
}
