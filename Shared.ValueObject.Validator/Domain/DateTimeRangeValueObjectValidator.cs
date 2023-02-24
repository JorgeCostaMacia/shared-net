using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class DateTimeRangeValueObjectValidator : AbstractValidator<DateTimeRangeValueObject>
    {
        public DateTimeRangeValueObjectValidator()
        {
            RuleFor(v => v.ValueStart)
                .GreaterThanOrEqualTo(new DateTime(1965, 1, 1, 0, 0, 0, 0))
                .LessThanOrEqualTo(v => v.ValueEnd)
                .WithName("DateTimeRangeValueObject.Start");

            RuleFor(v => v.ValueEnd)
                .GreaterThanOrEqualTo(v => v.ValueStart)
                .LessThanOrEqualTo(new DateTime(2200, 1, 1, 0, 0, 0, 0))
                .WithName("DateTimeRangeValueObject.End");
        }

        protected override void RaiseValidationException(ValidationContext<DateTimeRangeValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            DateTime value = new DateTime();

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (DateTime)error.AttemptedValue;
            }

            throw new DateTimeRangeValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
