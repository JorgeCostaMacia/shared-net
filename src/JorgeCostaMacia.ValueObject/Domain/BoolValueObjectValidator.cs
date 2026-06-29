using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="BoolValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator is designed to perform validation rules specific to the <see cref="BoolValueObject"/> instance.
/// As a boolean Value Object, it typically has few intrinsic validation rules (the <see cref="bool"/> type itself is simple).
/// However, this class is essential for enforcing custom validation logic in derived classes or when integrating
/// the <see cref="BoolValueObject"/> into a larger validation chain.
/// </para>
/// </remarks>
public class BoolValueObjectValidator : AbstractValidator<BoolValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BoolValueObjectValidator"/> class.
    /// </summary>
    /// <remarks>
    /// Validation rules (if any) should be defined here. For the base <see cref="BoolValueObject"/>, this constructor usually remains empty,
    /// relying on derived classes to add constraints (e.g., <c>RuleFor(v => v.Value).Must(v => v == true)</c>).
    /// </remarks>
    public BoolValueObjectValidator() { }

    /// <summary>
    /// Overrides the default behavior of FluentValidation to throw a custom domain exception
    /// (<see cref="BoolValueObjectValidationException"/>) upon validation failure, instead of the default <see cref="ValidationException"/>.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="BoolValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<BoolValueObject> context, ValidationResult result)
        => throw new BoolValueObjectValidationException(result.Errors);
}