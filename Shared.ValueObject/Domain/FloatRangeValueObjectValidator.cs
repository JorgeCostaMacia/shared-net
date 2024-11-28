using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class FloatRangeValueObjectValidator : AbstractValidator<FloatRangeValueObject>, Shared.Validator.Domain.IValidator
{
    public FloatRangeValueObjectValidator(IValidator<FloatValueObject> validator)
    {
        RuleFor(v => v.ValueStart)
             .SetValidator(validator)
             .WithName(v => v.GetType().FullName + "." + v.ValueEnd.GetType().Name + ".End");

        RuleFor(v => v.ValueEnd)
            .SetValidator(validator)
            .WithName(v => v.GetType().FullName + "." + v.ValueEnd.GetType().Name + ".End");

        RuleFor(v => v.ValueStart.Value)
            .LessThanOrEqualTo(v => v.ValueEnd.Value)
            .WithName(v => v.GetType().FullName + "." + v.ValueStart.GetType().Name + ".End");

        RuleFor(v => v.ValueEnd.Value)
            .GreaterThanOrEqualTo(v => v.ValueStart.Value)
            .WithName(v => v.GetType().FullName + "." + v.ValueEnd.GetType().Name + ".End");
    }

    protected override void RaiseValidationException(ValidationContext<FloatRangeValueObject> context, ValidationResult result) => throw new FloatRangeValueObjectConstraintException(null, result.Errors);
}
