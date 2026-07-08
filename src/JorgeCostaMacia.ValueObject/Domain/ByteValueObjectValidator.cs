using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="ByteValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator performs validation rules specific to the encapsulated binary data,
/// such as checking for null/empty arrays, size limits, or specific content constraints.
/// </para>
/// <para>
/// For the base implementation, this class usually contains general rules applicable to all binary data
/// (e.g., ensuring the byte array is not null), relying on derived validators to add specific rules
/// like maximum file size or content type validation.
/// </para>
/// </remarks>
public class ByteValueObjectValidator : AbstractValidator<ByteValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ByteValueObjectValidator"/> class.
    /// </summary>
    /// <remarks>
    /// Validation rules (if any) should be defined here. For the base <see cref="ByteValueObject"/>, rules are typically minimal,
    /// focusing on ensuring the existence and integrity of the byte array before more specific constraints are applied by derived classes.
    /// </remarks>
    public ByteValueObjectValidator() { }

    /// <summary>
    /// Overrides the default behavior of FluentValidation to throw a custom domain exception
    /// (<see cref="ByteValueObjectValidationException"/>) upon validation failure, instead of the default <see cref="ValidationException"/>.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="ByteValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<ByteValueObject> context, ValidationResult result)
        => throw new ByteValueObjectValidationException(result.Errors);
}
