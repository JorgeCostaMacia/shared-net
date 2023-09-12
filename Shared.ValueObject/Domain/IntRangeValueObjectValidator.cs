using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class IntRangeValueObjectValidator : AbstractValidator<IntRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public IntRangeValueObjectValidator(string nameStart = "IntRangeValueObject.Start", string nameEnd = "IntRangeValueObject.End")
        {
            Include(new RangeValueObjectValidator<int>(nameStart, nameEnd));

            RuleFor(v => v.ValueStart)
                .GreaterThanOrEqualTo(int.MinValue)
                .LessThanOrEqualTo(v => v.ValueEnd)
                .WithName(nameStart);

            RuleFor(v => v.ValueEnd)
                .GreaterThanOrEqualTo(v => v.ValueStart)
                .LessThanOrEqualTo(int.MaxValue)
                .WithName(nameEnd);
        }

        protected override void RaiseValidationException(ValidationContext<IntRangeValueObject> context, ValidationResult result) => throw new IntRangeValueObjectConstraintException(result.Errors);
    }
}
