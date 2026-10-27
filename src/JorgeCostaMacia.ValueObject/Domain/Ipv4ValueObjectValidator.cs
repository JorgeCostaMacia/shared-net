using System.Net;
using System.Net.Sockets;
using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="Ipv4ValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator accepts an IPv4 address only in its canonical dotted-decimal form: four decimal octets, without
/// leading zeros. <see cref="IPAddress.TryParse(string, out IPAddress)"/> alone is not enough, because it follows
/// <c>inet_addr</c>: <c>"1.2.3"</c> parses as <c>1.2.0.3</c>, <c>"4294967295"</c> as <c>255.255.255.255</c>,
/// <c>"0x1.0x2.0x3.0x4"</c> as <c>1.2.3.4</c> and, worst of all, <c>"010.0.0.1"</c> as <c>8.0.0.1</c> — a leading zero
/// reads as octal. Requiring the text to equal the parsed address's own representation rules all of them out, and
/// with them every IPv6 address.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Inherited Validation:</b> Includes all base validation rules defined in the injected
///         <see cref="IValidator{T}"/> for the <see cref="StringValueObject"/>.
///     </description></item>
///     <item><description>
///         <b>Not Empty:</b> Ensures the IPv4 address string is not null or empty.
///     </description></item>
///     <item><description>
///         <b>IPv4 Format Check:</b> Ensures the string parses as an IPv4 address and is exactly that address's
///         canonical form.
///     </description></item>
/// </list>
/// </remarks>
public class Ipv4ValueObjectValidator : AbstractValidator<Ipv4ValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Ipv4ValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="StringValueObject"/> used to inherit base
    /// validation rules.</param>
    public Ipv4ValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
            .NotEmpty()
            .Must(v => IPAddress.TryParse(v, out IPAddress? ip)
                && ip.AddressFamily == AddressFamily.InterNetwork
                && ip.ToString() == v)
            .WithErrorCode("Ipv4Validator")
            .WithMessage("{PropertyName} must be an IPv4");
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="Ipv4ValueObjectValidator"/> instance.</returns>
    public static Ipv4ValueObjectValidator Create()
        => new Ipv4ValueObjectValidator(StringValueObjectValidator.Create());

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="Ipv4ValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="Ipv4ValueObjectValidationException">Thrown when the validation fails, encapsulating the
    /// <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(
        ValidationContext<Ipv4ValueObject> context,
        ValidationResult result
    ) => throw new Ipv4ValueObjectValidationException(result.Errors);
}
