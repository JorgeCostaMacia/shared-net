using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class IntRangeValueObjectValidator : AbstractValidator<IntRangeValueObject>, Shared.Validator.Domain.IValidator
{
    public IntRangeValueObjectValidator(string name = "IntRangeValueObject")
    {
        RuleFor(v => v.ValueStart)
            .SetValidator(new IntValueObjectValidator(name + ".End"))
            .WithName(name + ".End");

        RuleFor(v => v.ValueEnd)
            .SetValidator(new IntValueObjectValidator(name + ".End"))
            .WithName(name + ".End");

        RuleFor(v => v.ValueStart.Value)
            .LessThanOrEqualTo(v => v.ValueEnd.Value)
            .WithName(name + ".Start");

        RuleFor(v => v.ValueEnd.Value)
            .GreaterThanOrEqualTo(v => v.ValueStart.Value)
            .WithName(name + ".End");
    }

    protected override void RaiseValidationException(ValidationContext<IntRangeValueObject> context, ValidationResult result) => throw new IntRangeValueObjectConstraintException(null, result.Errors);
}
