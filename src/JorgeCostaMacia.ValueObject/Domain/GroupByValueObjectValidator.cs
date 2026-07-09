using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="GroupByValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator ensures the group-by criterion is not empty, and <b>includes</b> the base
/// <see cref="StringValueObjectValidator"/> rules via constructor injection, ensuring that any derived
/// String Value Object maintains its fundamental restrictions.
/// </para>
/// </remarks>
public class GroupByValueObjectValidator : AbstractValidator<GroupByValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupByValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="StringValueObject"/> used to inherit base validation rules.</param>
    public GroupByValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
             .NotEmpty();
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="GroupByValueObjectValidator"/> instance.</returns>
    public static GroupByValueObjectValidator Create() => new GroupByValueObjectValidator(StringValueObjectValidator.Create());

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="GroupByValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="GroupByValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<GroupByValueObject> context, ValidationResult result)
        => throw new GroupByValueObjectValidationException(result.Errors);
}
