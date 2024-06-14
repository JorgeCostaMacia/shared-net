using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class FloatRangeValueObjectValidator : AbstractValidator<FloatRangeValueObject>, Shared.Validator.Domain.IValidator
{
    public FloatRangeValueObjectValidator(string name = "FloatRangeValueObject")
    {
        RuleFor(v => v.ValueStart)
            .SetValidator(new FloatValueObjectValidator(name + ".End"))
            .WithName(name + ".End");

        RuleFor(v => v.ValueEnd)
            .SetValidator(new FloatValueObjectValidator(name + ".End"))
            .WithName(name + ".End");

        RuleFor(v => v.ValueStart.Value)
            .LessThanOrEqualTo(v => v.ValueEnd.Value)
            .WithName(name + ".Start");

        RuleFor(v => v.ValueEnd.Value)
            .GreaterThanOrEqualTo(v => v.ValueStart.Value)
            .WithName(name + ".End");
    }

    protected override void RaiseValidationException(ValidationContext<FloatRangeValueObject> context, ValidationResult result) => throw new FloatRangeValueObjectConstraintException(null, result.Errors);
}
