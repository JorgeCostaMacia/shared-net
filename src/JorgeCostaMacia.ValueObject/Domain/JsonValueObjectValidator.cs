using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="JsonValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces constraints to ensure the encapsulated string value represents
/// syntactically valid JSON data (either an object or an array).
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Inherited Validation:</b> Includes all base validation rules defined in the injected <see cref="IValidator{T}"/> for the <see cref="StringValueObject"/>.
///     </description></item>
///     <item><description>
///         <b>Not Empty:</b> Ensures the string value is not null or empty.
///     </description></item>
///     <item><description>
///         <b>Minimum Length:</b> Enforces a minimum length of 2 characters (e.g., "{}").
///     </description></item>
///     <item><description>
///         <b>JSON Format Check:</b> Uses a regular expression to verify that the value, optionally surrounded by whitespace,
///         starts and ends with braces (`{}`) for an object or brackets (`[]`) for an array.
///     </description></item>
/// </list>
/// </remarks>
public class JsonValueObjectValidator : AbstractValidator<JsonValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="StringValueObject"/> used to inherit base validation rules.</param>
    public JsonValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
             .NotEmpty()
             .MinimumLength(2)
             .Must(v => Regex.IsMatch(v, @"^\s*(\{.*\}|\[.*\])\s*$"))
             .WithErrorCode("JsonValidator")
             .WithMessage("{PropertyName} must be a JSON");
    }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="JsonValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="JsonValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<JsonValueObject> context, ValidationResult result)
        => throw new JsonValueObjectValidationException(result.Errors);
}