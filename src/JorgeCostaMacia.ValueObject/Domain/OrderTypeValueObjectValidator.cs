using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="OrderTypeValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces constraints that limit the possible values of the order type to
/// recognized sorting directions, typically "ASC" or "DESC".
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Inherited Validation:</b> Includes all base validation rules defined in the injected <see cref="IValidator{T}"/> for the <see cref="StringValueObject"/>.
///     </description></item>
///     <item><description>
///         <b>Not Empty:</b> Ensures the order type string is not null or empty.
///     </description></item>
///     <item><description>
///         <b>Permitted Values:</b> Ensures the encapsulated value matches exactly either "ASC" (Ascending) or "DESC" (Descending).
///         (Note: The <see cref="OrderTypeValueObject"/> ensures the value is already uppercase).
///     </description></item>
/// </list>
/// </remarks>
public class OrderTypeValueObjectValidator : AbstractValidator<OrderTypeValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderTypeValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="StringValueObject"/> used to inherit base validation rules.</param>
    public OrderTypeValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
             .NotEmpty() // Must contain data
             .Must(v2 => v2 == "ASC" || v2 == "DESC")
             .WithMessage("{PropertyName} must be 'ASC' or 'DESC'");
    }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="OrderTypeValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="OrderTypeValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<OrderTypeValueObject> context, ValidationResult result)
        => throw new OrderTypeValueObjectValidationException(result.Errors);
}
