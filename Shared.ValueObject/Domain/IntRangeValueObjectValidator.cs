using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class IntRangeValueObjectValidator : AbstractValidator<IntRangeValueObject>, Shared.Validator.Domain.IValidator
{
    public IntRangeValueObjectValidator(IValidator<IntValueObject> validator)
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

    protected override void RaiseValidationException(ValidationContext<IntRangeValueObject> context, ValidationResult result) => throw new IntRangeValueObjectConstraintException(null, null, null, null, null, null, result.Errors);
}
