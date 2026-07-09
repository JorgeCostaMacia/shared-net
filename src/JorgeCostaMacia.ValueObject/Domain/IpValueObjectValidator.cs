using System.Net;
using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="IpValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces basic constraints suitable for IP addresses (both IPv4 and potentially IPv6, though rules are tailored for IPv4 length).
/// It leverages internal .NET classes (<see cref="IPAddress.TryParse(string, out IPAddress)"/>) to ensure the encapsulated string is a structurally valid IP address.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Inherited Validation:</b> Includes all base validation rules defined in the injected <see cref="IValidator{T}"/> for the <see cref="StringValueObject"/>.
///     </description></item>
///     <item><description>
///         <b>Not Empty:</b> Ensures the IP address string is not null or empty.
///     </description></item>
///     <item><description>
///         <b>Length Check:</b> Enforces minimum (7 chars: e.g., "1.1.1.1") and maximum (15 chars: e.g., "255.255.255.255") length limits, primarily targeting IPv4 addresses.
///     </description></item>
///     <item><description>
///         <b>IP Format Check:</b> Uses a custom rule to ensure the string contains exactly three dots and is successfully parsable by <see cref="IPAddress.TryParse(string, out IPAddress)"/>.
///     </description></item>
/// </list>
/// </remarks>
public class IpValueObjectValidator : AbstractValidator<IpValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IpValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="StringValueObject"/> used to inherit base validation rules.</param>
    public IpValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
             .NotEmpty()
             .MinimumLength(7)
             .MaximumLength(15)
             .Must(v => string.IsNullOrEmpty(v) || (v.Count(c => c == '.') == 3 && IPAddress.TryParse(v, out _)))
             .WithErrorCode("IpValidator")
             .WithMessage("{PropertyName} must be a IP");
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="IpValueObjectValidator"/> instance.</returns>
    public static IpValueObjectValidator Create() => new IpValueObjectValidator(StringValueObjectValidator.Create());

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="IpValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="IpValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<IpValueObject> context, ValidationResult result)
        => throw new IpValueObjectValidationException(result.Errors);
}
