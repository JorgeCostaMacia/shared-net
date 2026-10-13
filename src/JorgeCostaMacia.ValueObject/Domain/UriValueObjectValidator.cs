using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="UriValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces the general rule of a URI Value Object: it is present and absolute, whatever its scheme.
/// A derived Value Object that needs a narrower rule (only <c>http</c>/<c>https</c>, for one) includes this validator
/// and adds its own.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Not Null:</b> Ensures the URI is present.
///     </description></item>
///     <item><description>
///         <b>Absolute:</b> Ensures the URI is absolute (<see cref="Uri.IsAbsoluteUri"/>).
///     </description></item>
/// </list>
/// </remarks>
public class UriValueObjectValidator : AbstractValidator<UriValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UriValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    public UriValueObjectValidator()
    {
        RuleFor(v => v.Value)
            .NotNull()
            .Must(v => v is not null && v.IsAbsoluteUri)
            .WithErrorCode("UriValidator")
            .WithMessage("{PropertyName} must be an absolute Uri");
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator.
    /// </summary>
    /// <returns>A new <see cref="UriValueObjectValidator"/> instance.</returns>
    public static UriValueObjectValidator Create() => new UriValueObjectValidator();

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="UriValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="UriValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<UriValueObject> context, ValidationResult result)
        => throw new UriValueObjectValidationException(result.Errors);
}
