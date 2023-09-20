using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class FloatRangeValueObjectValidator : AbstractValidator<FloatRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public FloatRangeValueObjectValidator(string nameStart = "FloatRangeValueObject.Start", string nameEnd = "FloatRangeValueObject.End")
        {
            RuleFor(v => v.ValueStart)
                .SetValidator(new FloatValueObjectValidator(nameEnd))
                .WithName(nameEnd);

            RuleFor(v => v.ValueEnd)
                .SetValidator(new FloatValueObjectValidator(nameEnd))
                .WithName(nameEnd);

            RuleFor(v => v.ValueStart.Value)
                .LessThanOrEqualTo(v => v.ValueEnd.Value)
                .WithName(nameStart);

            RuleFor(v => v.ValueEnd.Value)
                .GreaterThanOrEqualTo(v => v.ValueStart.Value)
                .WithName(nameEnd);
        }

        protected override void RaiseValidationException(ValidationContext<FloatRangeValueObject> context, ValidationResult result) => throw new FloatRangeValueObjectConstraintException(result.Errors);
    }
}
