using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="OrderByValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces basic constraints suitable for ordering criteria strings,
/// leveraging validation rules defined in the base <see cref="StringValueObject"/> validator.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Inherited Validation:</b> Includes all base validation rules defined in the injected <see cref="IValidator{T}"/> for the <see cref="StringValueObject"/>.
///     </description></item>
///     <item><description>
///         <b>Not Empty:</b> Ensures the ordering criteria string is not null or empty.
///     </description></item>
/// </list>
/// </remarks>
public class OrderByValueObjectValidator : AbstractValidator<OrderByValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderByValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="StringValueObject"/> used to inherit base validation rules.</param>
    public OrderByValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
             .NotEmpty();
    }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="OrderByValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="OrderByValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<OrderByValueObject> context, ValidationResult result)
        => throw new OrderByValueObjectValidationException(result.Errors);
}
