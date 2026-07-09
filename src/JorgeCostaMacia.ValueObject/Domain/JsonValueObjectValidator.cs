using System.Text.Json;
using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="JsonValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces constraints specific to JSON documents, ensuring that the encapsulated string
/// is not empty and parses as a JSON object or array (scalar roots like a bare string or number are rejected).
/// </para>
/// <para>
/// It <b>includes</b> the base <see cref="StringValueObjectValidator"/> rules via constructor injection,
/// ensuring that any derived String Value Object maintains its fundamental restrictions.
/// </para>
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
             .Must(BeJsonObjectOrArray)
             .WithErrorCode("JsonValidator")
             .WithMessage("{PropertyName} must be a JSON");
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="JsonValueObjectValidator"/> instance.</returns>
    public static JsonValueObjectValidator Create() => new JsonValueObjectValidator(StringValueObjectValidator.Create());

    /// <summary>
    /// Determines whether the supplied string parses as a JSON document whose root is an object or an array.
    /// </summary>
    /// <param name="value">The string to inspect.</param>
    /// <returns><c>true</c> when the string is a JSON object or array; otherwise <c>false</c>.</returns>
    private static bool BeJsonObjectOrArray(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        try
        {
            using JsonDocument document = JsonDocument.Parse(value);
            return document.RootElement.ValueKind is JsonValueKind.Object or JsonValueKind.Array;
        }
        catch (JsonException)
        {
            return false;
        }
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
