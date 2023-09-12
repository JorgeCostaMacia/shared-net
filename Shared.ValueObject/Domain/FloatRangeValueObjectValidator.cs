using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class FloatRangeValueObjectValidator : AbstractValidator<FloatRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public FloatRangeValueObjectValidator(string nameStart = "FloatRangeValueObject.Start", string nameEnd = "FloatRangeValueObject.End")
        {
            Include(new RangeValueObjectValidator<float>(nameStart, nameEnd));

            RuleFor(v => v.ValueStart)
                .GreaterThanOrEqualTo(float.MinValue)
                .LessThanOrEqualTo(v => v.ValueEnd)
                .WithName(nameStart);

            RuleFor(v => v.ValueEnd)
                .GreaterThanOrEqualTo(v => v.ValueStart)
                .LessThanOrEqualTo(float.MaxValue)
                .WithName(nameEnd);
        }

        protected override void RaiseValidationException(ValidationContext<FloatRangeValueObject> context, ValidationResult result) => throw new FloatRangeValueObjectConstraintException(result.Errors);
    }
}
