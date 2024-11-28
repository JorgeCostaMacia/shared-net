using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class IntRangeValueObjectValidator : AbstractValidator<IntRangeValueObject>, Shared.Validator.Domain.IValidator
{
    public IntRangeValueObjectValidator(IValidator<IntValueObject> validator)
    {
        RuleFor(v => v.ValueStart)
             .SetValidator(validator)
             .WithName(v => v.GetType().FullName + "." + v.ValueEnd.GetType().Name + ".Start");

        RuleFor(v => v.ValueEnd)
            .SetValidator(validator)
            .WithName(v => v.GetType().FullName + "." + v.ValueEnd.GetType().Name + ".End");

        RuleFor(v => v.ValueStart.Value)
            .LessThanOrEqualTo(v => v.ValueEnd.Value)
            .WithName(v => v.GetType().FullName + "." + v.ValueStart.GetType().Name + ".Start");

        RuleFor(v => v.ValueEnd.Value)
            .GreaterThanOrEqualTo(v => v.ValueStart.Value)
            .WithName(v => v.GetType().FullName + "." + v.ValueEnd.GetType().Name + ".End");

    }

    protected override void RaiseValidationException(ValidationContext<IntRangeValueObject> context, ValidationResult result) => throw new IntRangeValueObjectConstraintException(null, result.Errors);
}
