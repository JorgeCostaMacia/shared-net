using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class DateTimeRangeValueObjectValidator : AbstractValidator<DateTimeRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public DateTimeRangeValueObjectValidator(string nameStart = "DateTimeRangeValueObject.Start", string nameEnd = "DateTimeRangeValueObject.End")
        {
            Include(new RangeValueObjectValidator<DateTime>(nameStart, nameEnd));

            RuleFor(v => v.ValueStart)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date.AddYears(-100))
                .LessThanOrEqualTo(v => v.ValueEnd)
                .WithName(nameStart);

            RuleFor(v => v.ValueEnd)
                .GreaterThanOrEqualTo(v => v.ValueStart)
                .LessThanOrEqualTo(DateTime.UtcNow.Date.AddYears(100))
                .WithName(nameEnd);
        }

        protected override void RaiseValidationException(ValidationContext<DateTimeRangeValueObject> context, ValidationResult result) => throw new DateTimeRangeValueObjectConstraintException(result.Errors);
    }
}
