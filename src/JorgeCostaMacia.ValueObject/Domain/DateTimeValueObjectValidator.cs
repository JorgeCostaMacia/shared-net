using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="DateTimeValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator rules out what is not a date rather than what is an unlikely one. The floor of 1900 is there
/// for the zero dates other systems write when they have none: <c>0001-01-01</c> (.NET's default) and
/// <c>1899-12-30</c> (the zero of Delphi, OLE Automation and Excel). No real value in these systems is older.
/// </para>
/// <para>
/// There is no ceiling, on purpose. What is reasonable above depends on the field — today for a birth date, a few
/// years for a planned task, decades for a contract, <c>9999-12-31</c> where it marks an open end — and a derived
/// validator can add a rule but never remove one it includes, so a ceiling here would bind every date in every
/// consumer. A field that needs one declares it in its own Value Object.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Not Empty:</b> The date cannot be the default/empty value.
///     </description></item>
///     <item><description>
///         <b>Minimum Date:</b> The date must be greater than or equal to January 1st, 1900.
///     </description></item>
/// </list>
/// </remarks>
public class DateTimeValueObjectValidator : AbstractValidator<DateTimeValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    public DateTimeValueObjectValidator()
    {
        RuleFor(v => v.Value)
            .NotEmpty()
            .GreaterThanOrEqualTo(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator.
    /// This is the assembly the static <c>Create</c> factories of the Value Objects rely on.
    /// </summary>
    /// <returns>A new <see cref="DateTimeValueObjectValidator"/> instance.</returns>
    public static DateTimeValueObjectValidator Create() => new DateTimeValueObjectValidator();

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
