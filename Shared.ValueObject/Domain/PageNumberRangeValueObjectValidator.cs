using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class PageNumberRangeValueObjectValidator : AbstractValidator<PageNumberRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public PageNumberRangeValueObjectValidator(string nameStart = "PageNumberRangeValueObject.Start", string nameEnd = "PageNumberRangeValueObject.End")
        {
            Include(new IntRangeValueObjectValidator(nameStart, nameEnd));

            RuleFor(v => v.ValueStart.Value)
                 .NotEmpty()
                 .GreaterThanOrEqualTo(1)
                 .WithName(nameStart);

            RuleFor(v => v.ValueEnd.Value)
                 .NotEmpty()
                 .GreaterThanOrEqualTo(1)
                 .WithName(nameStart);
        }

        protected override void RaiseValidationException(ValidationContext<PageNumberRangeValueObject> context, ValidationResult result) => throw new PageNumberRangeValueObjectConstraintException(result.Errors);
    }
}
