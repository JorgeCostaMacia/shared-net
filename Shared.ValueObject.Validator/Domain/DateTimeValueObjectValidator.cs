using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class DateTimeValueObjectValidator : AbstractValidator<DateTimeValueObject>, Shared.Validator.Domain.IValidator
    {
        public DateTimeValueObjectValidator(string name = "DateTimeValueObject")
        {
            RuleFor(v => v.Value)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date.AddYears(-100))
                .LessThanOrEqualTo(DateTime.UtcNow.Date.AddYears(100))
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<DateTimeValueObject> context, ValidationResult result) => throw new DateTimeValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
