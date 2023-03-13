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

        protected override void RaiseValidationException(ValidationContext<FloatValueObject> context, ValidationResult result) => throw new FloatValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
