using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class DateTimeValueObjectValidator : AbstractValidator<DateTimeValueObject>, Shared.Validator.Domain.IValidator
    {
        public DateTimeValueObjectValidator(string name = "DateTimeValueObject")
        {
            RuleFor(v => v.Value)
                .GreaterThanOrEqualTo(new DateTime(1900, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc).Date)
                .LessThanOrEqualTo(DateTime.UtcNow.Date.AddYears(100))
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<DateTimeValueObject> context, ValidationResult result) => throw new DateTimeValueObjectConstraintException(result.Errors);
    }
}
