using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class PageNumberRangeValueObjectValidator : AbstractValidator<PageNumberRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public PageNumberRangeValueObjectValidator(string nameStart = "PageNumberRangeValueObject.Start", string nameEnd = "PageNumberRangeValueObject.End")
        {
            Include(new IntRangeValueObjectValidator(nameStart, nameEnd));
        }

        protected override void RaiseValidationException(ValidationContext<PageNumberRangeValueObject> context, ValidationResult result) => throw new PageNumberRangeValueObjectConstraintException(result.Errors);
    }
}
