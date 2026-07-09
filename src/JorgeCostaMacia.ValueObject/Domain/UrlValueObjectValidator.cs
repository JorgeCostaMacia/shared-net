using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="UrlValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces constraints specific to <b>web</b> URLs: the encapsulated string must not be
/// empty and must be a valid absolute URI whose scheme is <c>http</c> or <c>https</c>. The scheme
/// allow-list rejects dangerous values (<c>javascript:</c>, <c>data:</c>, <c>file://</c> — including
/// plain filesystem paths, which would otherwise parse platform-dependently) so a validated
/// <see cref="UrlValueObject"/> is safe to emit into links and redirects.
/// </para>
/// <para>
/// Value Objects that need other schemes (e.g. <c>ftp</c>) derive their own type and validator with
/// their own scheme rule. It <b>includes</b> the base <see cref="StringValueObjectValidator"/> rules
/// via constructor injection, ensuring that any derived String Value Object maintains its fundamental restrictions.
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
            .Must(v => Uri.TryCreate(v, UriKind.Absolute, out Uri? uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            .WithErrorCode("UrlValidator")
            .WithMessage("{PropertyName} must be an http(s) Url");
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="UrlValueObjectValidator"/> instance.</returns>
    public static UrlValueObjectValidator Create() => new UrlValueObjectValidator(StringValueObjectValidator.Create());

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
