using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class PageNumberRangeValueObjectValidator : AbstractValidator<PageNumberRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public PageNumberRangeValueObjectValidator(string nameStart = "PageNumberRangeValueObject.Start", string nameEnd = "PageNumberRangeValueObject.End")
        {
            RuleFor(v => v.ValueStart)
                 .GreaterThanOrEqualTo(1)
                 .LessThanOrEqualTo(v => v.ValueEnd)
                 .WithName(nameStart);

            RuleFor(v => v.ValueEnd)
                .GreaterThanOrEqualTo(v => v.ValueStart)
                .LessThanOrEqualTo(int.MaxValue)
                .WithName(nameEnd);
        }

        protected override void RaiseValidationException(ValidationContext<PageNumberRangeValueObject> context, ValidationResult result)
        {
            int Value = result.Errors.Count > 0 ? (int)result.Errors.First().AttemptedValue : 0;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new PageNumberRangeValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
