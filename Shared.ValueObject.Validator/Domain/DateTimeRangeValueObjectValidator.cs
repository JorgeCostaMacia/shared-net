using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class DateTimeRangeValueObjectValidator : AbstractValidator<DateTimeRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public DateTimeRangeValueObjectValidator(string nameStart = "DateTimeRangeValueObject.Start", string nameEnd = "DateTimeRangeValueObject.End")
        {
            RuleFor(v => v.ValueStart)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date.AddYears(-100))
                .LessThanOrEqualTo(v => v.ValueEnd)
                .WithName(nameStart);

            RuleFor(v => v.ValueEnd)
                .GreaterThanOrEqualTo(v => v.ValueStart)
                .LessThanOrEqualTo(DateTime.UtcNow.Date.AddYears(100))
                .WithName(nameEnd);
        }



        protected override void RaiseValidationException(ValidationContext<DateTimeRangeValueObject> context, ValidationResult result) => throw new DateTimeRangeValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
