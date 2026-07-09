using FluentValidation;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// A concrete FluentValidation validator class for the <see cref="DateTimeRangeValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This validator enforces consistency within the date range, ensuring that the start date
/// is not chronologically after the end date.
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Nested Validation:</b> Applies an injected <see cref="IValidator{T}"/> to individually validate
///         both the <see cref="DateTimeRangeValueObject.ValueStart"/> and <see cref="DateTimeRangeValueObject.ValueEnd"/> properties.
///     </description></item>
///     <item><description>
///         <b>Range Invariant:</b> Contains rules to guarantee that the start date is less than or equal to the end date.
///     </description></item>
/// </list>
/// </remarks>
public class DateTimeRangeValueObjectValidator : AbstractValidator<DateTimeRangeValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeRangeValueObjectValidator"/> class and defines the validation rules.
    /// </summary>
    /// <param name="validator">The specific validator for <see cref="DateTimeValueObject"/> used to validate the range's individual components (start and end dates).</param>
    public DateTimeRangeValueObjectValidator(IValidator<DateTimeValueObject> validator)
    {
        RuleFor(v => v.ValueStart)
            .SetValidator(validator);

        RuleFor(v => v.ValueEnd)
            .SetValidator(validator);

        RuleFor(v => v.ValueStart.Value)
            .LessThanOrEqualTo(v => v.ValueEnd.Value);

        RuleFor(v => v.ValueEnd.Value)
            .GreaterThanOrEqualTo(v => v.ValueStart.Value);
    }

    /// <summary>
    /// Fabricates a self-contained, ready-to-use instance of the validator, chaining the
    /// <c>Create</c> factories of the validators it composes.
    /// </summary>
    /// <returns>A new <see cref="DateTimeRangeValueObjectValidator"/> instance.</returns>
    public static DateTimeRangeValueObjectValidator Create() => new DateTimeRangeValueObjectValidator(DateTimeValueObjectValidator.Create());

    /// <summary>
    /// Overrides the default FluentValidation exception mechanism to throw a custom domain exception
    /// (<see cref="DateTimeRangeValueObjectValidationException"/>) upon validation failure.
    /// </summary>
    /// <param name="context">The validation context containing the instance being validated.</param>
    /// <param name="result">The result object containing the list of validation failures.</param>
    /// <exception cref="DateTimeRangeValueObjectValidationException">Thrown when the validation fails, encapsulating the <see cref="ValidationResult.Errors"/>.</exception>
    protected override void RaiseValidationException(ValidationContext<DateTimeRangeValueObject> context, ValidationResult result)
        => throw new DateTimeRangeValueObjectValidationException(result.Errors);
}
