using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="DateTimeValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces common constraints on the encapsulated <see cref="DateTime"/> value,
/// ensuring that the date is a valid, meaningful date within a defined range.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Not Empty:</b> The date cannot be the default/empty value.
///     </description></item>
///     <item><description>
///         <b>Minimum Date:</b> The date must be greater than or equal to January 1st, 1900 UTC.
///     </description></item>
///     <item><description>
///         <b>Maximum Date:</b> The date must be less than or equal to 100 years from the current UTC date.
///     </description></item>
/// </list>
/// </remarks>
public class DateTimeValueObjectValidator : AbstractValidator<DateTimeValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <remarks>
    /// Rules are applied to enforce domain-level sanity checks on the date range and existence.
    /// </remarks>
    public DateTimeValueObjectValidator()
    {
        RuleFor(v => v.Value)
            .NotEmpty()
            .GreaterThanOrEqualTo(new DateTime(1900, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc).Date)
            .LessThanOrEqualTo(DateTime.UtcNow.Date.AddYears(100));
    }

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="DateTimeValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="DateTimeValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<DateTimeValueObject> context, ValidationResult result)
        => throw new DateTimeValueObjectValidationException(result.Errors);
}