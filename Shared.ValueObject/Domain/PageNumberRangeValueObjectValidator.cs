using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageNumberRangeValueObjectValidator : AbstractValidator<PageNumberRangeValueObject>, Shared.Validator.Domain.IValidator
{
    public PageNumberRangeValueObjectValidator(string name = "PageNumberRangeValueObject")
    {
        Include(new IntRangeValueObjectValidator(name));

        RuleFor(v => v.ValueStart.Value)
             .NotEmpty()
             .WithName(name + ".Start");

        RuleFor(v => v.ValueEnd.Value)
             .NotEmpty()
             .WithName(name + ".Start");
    }

    protected override void RaiseValidationException(ValidationContext<PageNumberRangeValueObject> context, ValidationResult result) => throw new PageNumberRangeValueObjectConstraintException(null, result.Errors);
}
