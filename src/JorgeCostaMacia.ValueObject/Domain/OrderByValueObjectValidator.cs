using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="OrderByValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator ensures the order-by criterion is not empty, and <b>includes</b> the base
/// <see cref="StringValueObjectValidator"/> rules via constructor injection, ensuring that any derived
/// String Value Object maintains its fundamental restrictions.
/// </para>
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
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="OrderByValueObjectValidator"/> instance.</returns>
    public static OrderByValueObjectValidator Create() => new OrderByValueObjectValidator(StringValueObjectValidator.Create());

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
