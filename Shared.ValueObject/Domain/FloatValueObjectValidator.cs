using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class FloatValueObjectValidator : AbstractValidator<FloatValueObject>, Shared.Validator.Domain.IValidator
    {
        public FloatValueObjectValidator(string name = "FloatValueObject")
        {
        }

        protected override void RaiseValidationException(ValidationContext<FloatValueObject> context, ValidationResult result) => throw new FloatValueObjectConstraintException(result.Errors);
    }
}
