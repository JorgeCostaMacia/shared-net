using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class FloatValueObjectValidator : AbstractValidator<FloatValueObject>
    {
        public FloatValueObjectValidator()
        {
            RuleFor(v => v.Value)
                  .GreaterThan(-2147483648)
                  .LessThan(2147483647)
                .WithName("FloatValueObject");
        }

        protected override void RaiseValidationException(ValidationContext<FloatValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            float value = 0;

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (float)error.AttemptedValue;
            }

            throw new FloatValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
