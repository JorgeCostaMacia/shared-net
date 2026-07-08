using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="UrlValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces constraints specific to URL format, ensuring that the encapsulated string
/// is not empty and represents a valid absolute URI whose scheme is not <c>file</c> — plain filesystem
/// paths are rejected. (Without that check the rule is platform-dependent: on Unix a rooted path like
/// <c>/foo/bar</c> parses as an absolute <c>file://</c> URI, on Windows <c>C:\foo</c> does.)
/// </para>
/// <para>
/// It <b>includes</b> the base <see cref="StringValueObjectValidator"/> rules via constructor injection,
/// ensuring that any derived String Value Object maintains its fundamental restrictions.
/// </para>
/// </remarks>
public class UrlValueObjectValidator : AbstractValidator<UrlValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UrlValueObjectValidator"/> class.
    /// </summary>
    /// <param name="validator">The base validator for <see cref="StringValueObject"/>, whose rules are included.</param>
    public UrlValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
            .NotEmpty()
            .MinimumLength(1)
            .Must(v => Uri.TryCreate(v, UriKind.Absolute, out Uri? uri) && !uri.IsFile)
            .WithErrorCode("UrlValidator")
            .WithMessage("{PropertyName} must be an Url");
    }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw the specific domain exception
    /// (<see cref="UrlValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="UrlValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<UrlValueObject> context, ValidationResult result)
        => throw new UrlValueObjectValidationException(result.Errors);
}
