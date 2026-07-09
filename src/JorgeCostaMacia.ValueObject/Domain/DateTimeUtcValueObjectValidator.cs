using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="DateTimeUtcValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator <b>includes</b> the base <see cref="DateTimeValueObjectValidator"/> rules via constructor
/// injection (not empty, minimum January 1st 1900, maximum 100 years from the current UTC date), so the
/// date-range knowledge lives once, in the base family. Rules specific to the UTC specialization (if any)
/// are defined within the constructor.
/// </para>
/// </remarks>
public class DateTimeUtcValueObjectValidator : AbstractValidator<DateTimeUtcValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeUtcValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="DateTimeValueObject"/> used to inherit base validation rules.</param>
    public DateTimeUtcValueObjectValidator(IValidator<DateTimeValueObject> validator)
    {
        Include(validator);
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="DateTimeUtcValueObjectValidator"/> instance.</returns>
    public static DateTimeUtcValueObjectValidator Create() => new DateTimeUtcValueObjectValidator(DateTimeValueObjectValidator.Create());

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="DateTimeUtcValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="DateTimeUtcValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<DateTimeUtcValueObject> context, ValidationResult result)
        => throw new DateTimeUtcValueObjectValidationException(result.Errors);
}
