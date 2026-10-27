using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="DateTimeValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator is a net for values that cannot be real dates. The floor is there for the
/// zero dates other systems write when they have none, and it is strict because one of them sits on it:
/// <c>0001-01-01</c> (.NET's default), <c>1753-01-01</c> (SQL Server's <c>datetime</c> minimum), <c>1899-12-30</c>
/// (the zero of Delphi, OLE Automation and Excel) and <c>1900-01-01 00:00</c> (SQL Server's <c>CAST(0 AS datetime)</c>,
/// Excel's serial 1). It also stops a year mistyped on the low side (<c>0215</c> for <c>2015</c>). No real value in
/// these systems is that old: a living person was born after 1900, and so were the grandparents of anyone of
/// child-bearing age.
/// </para>
/// <para>
/// The ceiling, a hundred years from today, is the same kind of net on the other side: a year mistyped high
/// (<c>2190</c> for <c>2019</c>) and the far-future sentinels. It is rolling, so it moves with the clock, and wide
/// enough for any real planned date. It binds every consumer — a derived validator can add a rule but never
/// remove one it includes — so an open-end marker such as <c>9999-12-31</c> does not pass; a field that needs a
/// tighter bound declares it in its own Value Object.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Not Empty:</b> The date cannot be the default/empty value.
///     </description></item>
///     <item><description>
///         <b>Minimum Date:</b> The date must be later than midnight on January 1st, 1900.
///     </description></item>
///     <item><description>
///         <b>Maximum Date:</b> The date must be less than or equal to 100 years from the current UTC date.
///     </description></item>
/// </list>
/// </remarks>
public class DateTimeValueObjectValidator : AbstractValidator<DateTimeValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeValueObjectValidator"/> class and defines the validation
    /// rules.
    /// </summary>
    public DateTimeValueObjectValidator()
    {
        RuleFor(v => v.Value)
            .NotEmpty()
            .GreaterThan(new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
            .LessThanOrEqualTo(DateTime.UtcNow.Date.AddYears(100));
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
    /// <exception cref="DateTimeValueObjectValidationException">Thrown when the validation fails, encapsulating the
    /// <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(
        ValidationContext<DateTimeValueObject> context,
        ValidationResult result
    ) => throw new DateTimeValueObjectValidationException(result.Errors);
}
