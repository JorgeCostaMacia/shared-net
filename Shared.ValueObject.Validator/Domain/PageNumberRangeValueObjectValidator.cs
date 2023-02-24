using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class PageNumberRangeValueObjectValidator : AbstractValidator<PageNumberRangeValueObject>
    {
        public PageNumberRangeValueObjectValidator()
        {
            RuleFor(v => v.ValueStart)
                 .GreaterThanOrEqualTo(0)
                 .LessThanOrEqualTo(v => v.ValueEnd)
                 .WithName("PageNumberRangeValueObject.Start");

            RuleFor(v => v.ValueEnd)
                .GreaterThanOrEqualTo(v => v.ValueStart)
                .LessThanOrEqualTo(2147483647)
                .WithName("PageNumberRangeValueObject.End");
        }

        protected override void RaiseValidationException(ValidationContext<PageNumberRangeValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            int value = 0;

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (int)error.AttemptedValue;
            }

            throw new PageNumberRangeValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
