using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class IntRangeValueObjectValidator : AbstractValidator<IntRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public IntRangeValueObjectValidator(string nameStart = "IntRangeValueObject.Start", string nameEnd = "IntRangeValueObject.End")
        {
            RuleFor(v => v.ValueStart)
                .SetValidator(new IntValueObjectValidator(nameEnd))
                .WithName(nameEnd);

            RuleFor(v => v.ValueEnd)
                .SetValidator(new IntValueObjectValidator(nameEnd))
                .WithName(nameEnd);

            RuleFor(v => v.ValueStart.Value)
                .LessThanOrEqualTo(v => v.ValueEnd.Value)
                .WithName(nameStart);

            RuleFor(v => v.ValueEnd.Value)
                .GreaterThanOrEqualTo(v => v.ValueStart.Value)
                .WithName(nameEnd);
        }

        protected override void RaiseValidationException(ValidationContext<IntRangeValueObject> context, ValidationResult result) => throw new IntRangeValueObjectConstraintException(result.Errors);
    }
}
