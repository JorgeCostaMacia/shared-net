using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class FloatRangeValueObjectValidator : AbstractValidator<FloatRangeValueObject>, Shared.Validator.Domain.IValidator
{
    public FloatRangeValueObjectValidator(IValidator<FloatValueObject> validator)
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

    protected override void RaiseValidationException(ValidationContext<FloatRangeValueObject> context, ValidationResult result) => throw new FloatRangeValueObjectConstraintException(null, result.Errors);
}
