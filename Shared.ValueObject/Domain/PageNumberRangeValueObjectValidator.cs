using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageNumberRangeValueObjectValidator : AbstractValidator<PageNumberRangeValueObject>, Shared.Validator.Domain.IValidator
{
    public PageNumberRangeValueObjectValidator(IValidator<PageNumberValueObject> validator)
    {
        RuleFor(v => v.ValueStart)
             .SetValidator(validator);

        RuleFor(v => v.ValueEnd)
            .SetValidator(validator);

        RuleFor(v => v.ValueStart.Value)
            .LessThanOrEqualTo(v => v.ValueEnd.Value);

        RuleFor(v => v.ValueEnd.Value)
            .GreaterThanOrEqualTo(v => v.ValueStart.Value);
    }

    protected override void RaiseValidationException(ValidationContext<PageNumberRangeValueObject> context, ValidationResult result) => throw new PageNumberRangeValueObjectConstraintException(null, result.Errors);
}
