using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class DateTimeValueObjectValidator : AbstractValidator<DateTimeValueObject>, Shared.Validator.Domain.IValidator
{
    public DateTimeValueObjectValidator()
    {
        RuleFor(v => v.Value)
            .NotEmpty()
            .GreaterThanOrEqualTo(new DateTime(1900, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc).Date)
            .LessThanOrEqualTo(DateTime.UtcNow.Date.AddYears(100))
            .WithName(v => v.GetType().FullName);
    }

    protected override void RaiseValidationException(ValidationContext<DateTimeValueObject> context, ValidationResult result) => throw new DateTimeValueObjectConstraintException(null, result.Errors);
}
