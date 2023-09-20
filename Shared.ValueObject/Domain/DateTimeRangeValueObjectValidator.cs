using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class DateTimeRangeValueObjectValidator : AbstractValidator<DateTimeRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public DateTimeRangeValueObjectValidator(string nameStart = "DateTimeRangeValueObject.Start", string nameEnd = "DateTimeRangeValueObject.End")
        {
            RuleFor(v => v.ValueStart)
                .SetValidator(new DateTimeValueObjectValidator(nameEnd))
                .WithName(nameEnd);

            RuleFor(v => v.ValueEnd)
                .SetValidator(new DateTimeValueObjectValidator(nameEnd))
                .WithName(nameEnd);

            RuleFor(v => v.ValueStart.Value)
                .LessThanOrEqualTo(v => v.ValueEnd.Value)
                .WithName(nameStart);

            RuleFor(v => v.ValueEnd.Value)
                .GreaterThanOrEqualTo(v => v.ValueStart.Value)
                .WithName(nameEnd);
        }

        protected override void RaiseValidationException(ValidationContext<DateTimeRangeValueObject> context, ValidationResult result) => throw new DateTimeRangeValueObjectConstraintException(result.Errors);
    }
}
