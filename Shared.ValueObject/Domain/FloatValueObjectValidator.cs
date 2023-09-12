using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class FloatValueObjectValidator : AbstractValidator<FloatValueObject>, Shared.Validator.Domain.IValidator
    {
        public FloatValueObjectValidator(string name = "FloatValueObject")
        {
            RuleFor(v => v.Value)
                .GreaterThanOrEqualTo(float.MinValue)
                .LessThanOrEqualTo(float.MaxValue)
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<FloatValueObject> context, ValidationResult result) => throw new FloatValueObjectConstraintException(result.Errors);
    }
}
