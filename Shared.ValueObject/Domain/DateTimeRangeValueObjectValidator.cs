using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class DateTimeRangeValueObjectValidator : AbstractValidator<DateTimeRangeValueObject>, Shared.Validator.Domain.IValidator
{
    public DateTimeRangeValueObjectValidator(IValidator<DateTimeValueObject> validator)
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

    protected override void RaiseValidationException(ValidationContext<DateTimeRangeValueObject> context, ValidationResult result) => throw new DateTimeRangeValueObjectConstraintException(null, null, null, null, null, null, result.Errors);
}
