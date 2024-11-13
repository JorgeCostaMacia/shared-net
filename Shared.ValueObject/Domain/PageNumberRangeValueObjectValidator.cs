using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageNumberRangeValueObjectValidator : AbstractValidator<PageNumberRangeValueObject>, Shared.Validator.Domain.IValidator
{
    public PageNumberRangeValueObjectValidator(IValidator<PageNumberValueObject> validator)
    {
        RuleFor(v => v.ValueStart)
             .SetValidator(validator)
             .WithName(v => v.GetType().Name + "." + v.ValueEnd.GetType().Name + ".End");

        RuleFor(v => v.ValueEnd)
            .SetValidator(validator)
            .WithName(v => v.GetType().Name + "." + v.ValueEnd.GetType().Name + ".End");

        RuleFor(v => v.ValueStart.Value)
            .LessThanOrEqualTo(v => v.ValueEnd.Value)
            .WithName(v => v.GetType().Name + "." + v.ValueStart.GetType().Name + ".End");

        RuleFor(v => v.ValueEnd.Value)
            .GreaterThanOrEqualTo(v => v.ValueStart.Value)
            .WithName(v => v.GetType().Name + "." + v.ValueEnd.GetType().Name + ".End");
    }

    protected override void RaiseValidationException(ValidationContext<PageNumberRangeValueObject> context, ValidationResult result) => throw new PageNumberRangeValueObjectConstraintException(null, result.Errors);
}
