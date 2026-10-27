using System.Net.Mail;
using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="EmailValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator accepts a single email address as the standard defines it, and nothing around it.
/// FluentValidation's <c>EmailAddress</c> rule is not used: it only checks for one <c>@</c> that is neither first nor
/// last, so <c>"a b@c.com"</c>, <c>"Name &lt;a@b.com&gt;"</c> or <c>"a..b@c.com"</c> pass. That naivety is deliberate
/// for a
/// form backed by a confirmation email; a Value Object has no such flow behind it. The address is parsed with
/// <see cref="MailAddress"/> instead, and the text has to equal the address it parsed to: on its own,
/// <see cref="MailAddress.TryCreate(string, out MailAddress)"/> reads <c>"a b@c.com"</c> as the display name <c>a</c>
/// plus <c>b@c.com</c>, and a comma list as one of its addresses.
/// </para>
/// <para>
/// The base takes every address the standard does, the rare ones included: a dotless domain (<c>a@localhost</c>),
/// an IP literal, a quoted local part, a non-ASCII domain. It also keeps the case as it arrived. A derived Value
/// Object that needs a narrower rule (an address deliverable on the internet, a fixed domain) or a normalized one
/// (lower case, to compare) adds it.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Inherited Validation:</b> Includes all base validation rules defined in the injected
///         <see cref="IValidator{T}"/> for the <see cref="StringValueObject"/>.
///     </description></item>
///     <item><description>
///         <b>Not Empty:</b> Ensures the email value is not null or empty.
///     </description></item>
///     <item><description>
///         <b>Email Format Check:</b> Ensures the string parses as one email address and is exactly that address.
///     </description></item>
/// </list>
/// </remarks>
public class EmailValueObjectValidator : AbstractValidator<EmailValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailValueObjectValidator"/> class and defines the validation
    /// rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="StringValueObject"/> used to inherit base
    /// validation rules.</param>
    public EmailValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
            .NotEmpty()
            .Must(v => MailAddress.TryCreate(v, out MailAddress? address) && address.Address == v)
            .WithErrorCode("EmailValidator")
            .WithMessage("{PropertyName} must be an email");
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="EmailValueObjectValidator"/> instance.</returns>
    public static EmailValueObjectValidator Create()
        => new EmailValueObjectValidator(StringValueObjectValidator.Create());

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="EmailValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="EmailValueObjectValidationException">Thrown when the validation fails, encapsulating the
    /// <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(
        ValidationContext<EmailValueObject> context,
        ValidationResult result
    ) => throw new EmailValueObjectValidationException(result.Errors);
}
