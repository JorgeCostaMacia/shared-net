using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class DateTimeRangeValueObjectValidator : AbstractValidator<DateTimeRangeValueObject>, Shared.Validator.Domain.IValidator
{
    public DateTimeRangeValueObjectValidator(string name = "DateTimeRangeValueObject")
    {
        RuleFor(v => v.ValueStart)
            .SetValidator(new DateTimeValueObjectValidator(name + ".End"))
            .WithName(name + ".End");

        RuleFor(v => v.ValueEnd)
            .SetValidator(new DateTimeValueObjectValidator(name + ".End"))
            .WithName(name + ".End");

        RuleFor(v => v.ValueStart.Value)
            .LessThanOrEqualTo(v => v.ValueEnd.Value)
            .WithName(name + ".Start");

        RuleFor(v => v.ValueEnd.Value)
            .GreaterThanOrEqualTo(v => v.ValueStart.Value)
            .WithName(name + ".End");
    }

    protected override void RaiseValidationException(ValidationContext<DateTimeRangeValueObject> context, ValidationResult result) => throw new DateTimeRangeValueObjectConstraintException(null, result.Errors);
}
