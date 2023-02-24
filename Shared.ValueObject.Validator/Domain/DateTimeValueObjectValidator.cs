using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class DateTimeValueObjectValidator : AbstractValidator<DateTimeValueObject>
    {
        public DateTimeValueObjectValidator()
        {
            RuleFor(v => v.Value)
                .GreaterThanOrEqualTo(new DateTime(1965, 1, 1, 0, 0, 0, 0))
                .LessThanOrEqualTo(new DateTime(2200, 1, 1, 0, 0, 0, 0))
                .WithName("DateTimeValueObject");
        }

        protected override void RaiseValidationException(ValidationContext<DateTimeValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            DateTime value = new DateTime();

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (DateTime)error.AttemptedValue;
            }

            throw new DateTimeValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
