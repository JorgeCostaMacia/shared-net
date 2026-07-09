using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="UuidValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces basic constraints specific to GUID/UUID format, ensuring that the encapsulated value
/// is not an empty GUID (<c>Guid.Empty</c>).
/// </para>
/// <para>
/// This validator acts as the fundamental constraint enforcer for all Value Objects based on <see cref="Guid"/>.
/// </para>
/// </remarks>
public class UuidValueObjectValidator : AbstractValidator<UuidValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UuidValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <remarks>
    /// The primary rule ensures that the GUID value is not <see cref="Guid.Empty"/>.
    /// </remarks>
    public UuidValueObjectValidator()
    {
        RuleFor(v => v.Value)
            .NotEmpty();
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator.
    /// This is the assembly the static <c>Create</c> factories of the Value Objects rely on.
    /// </summary>
    /// <returns>A new <see cref="UuidValueObjectValidator"/> instance.</returns>
    public static UuidValueObjectValidator Create() => new UuidValueObjectValidator();

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw the specific domain exception
    /// (<see cref="UuidValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="UuidValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<UuidValueObject> context, ValidationResult result)
        => throw new UuidValueObjectValidationException(result.Errors);
}
