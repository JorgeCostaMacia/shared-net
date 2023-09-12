using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class DecimalValueObjectValidator : AbstractValidator<DecimalValueObject>, Shared.Validator.Domain.IValidator
    {
        public DecimalValueObjectValidator(string name = "DecimalValueObject")
        {
            RuleFor(v => v.Value)
                .GreaterThanOrEqualTo(decimal.MinValue)
                .LessThanOrEqualTo(decimal.MaxValue)
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<DecimalValueObject> context, ValidationResult result) => throw new DecimalValueObjectConstraintException(result.Errors);
    }
}
