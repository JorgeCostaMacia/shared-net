using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="UriValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces only the general rule of a URI Value Object: it is present. A <see cref="Uri"/> cannot be
/// malformed — building one from text that is not a URI throws <see cref="UriFormatException"/> before validation —
/// so the base accepts any URI, relative or absolute, of any scheme.
/// </para>
/// <para>
/// A derived Value Object that needs a narrower rule includes this validator and adds its own: an absolute URI
/// (<see cref="Uri.IsAbsoluteUri"/>), a web URL (absolute and <c>http</c>/<c>https</c>), a relative path. A rule on
/// the scheme must check <see cref="Uri.IsAbsoluteUri"/> first, because <see cref="Uri.Scheme"/> throws on a relative
/// URI.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Not Null:</b> Ensures the URI is present.
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
            .NotNull();
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
    /// <exception cref="UriValueObjectValidationException">Thrown when the validation fails, encapsulating the
    /// <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(
        ValidationContext<UriValueObject> context,
        ValidationResult result
    ) => throw new UriValueObjectValidationException(result.Errors);
}
