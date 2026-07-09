using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="OrderTypeValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator ensures the ordering direction is not empty and is one of the allowed values
/// (<c>ASC</c> or <c>DESC</c>). It <b>includes</b> the base <see cref="StringValueObjectValidator"/> rules
/// via constructor injection, ensuring that any derived String Value Object maintains its fundamental restrictions.
/// </para>
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
             .NotEmpty()
             .Must(v2 => v2 == "ASC" || v2 == "DESC")
             .WithMessage("{PropertyName} must be 'ASC' or 'DESC'");
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="OrderTypeValueObjectValidator"/> instance.</returns>
    public static OrderTypeValueObjectValidator Create() => new OrderTypeValueObjectValidator(StringValueObjectValidator.Create());

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
