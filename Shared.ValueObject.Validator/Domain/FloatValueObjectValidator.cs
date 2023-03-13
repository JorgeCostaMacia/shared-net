using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
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

        protected override void RaiseValidationException(ValidationContext<FloatValueObject> context, ValidationResult result)
        {
            float Value = result.Errors.Count > 0 ? (float)result.Errors.First().AttemptedValue : 0;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new FloatValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
