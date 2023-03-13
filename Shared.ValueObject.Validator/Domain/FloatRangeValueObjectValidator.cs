using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class FloatRangeValueObjectValidator : AbstractValidator<FloatRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public FloatRangeValueObjectValidator(string nameStart = "FloatRangeValueObject.Start", string nameEnd = "FloatRangeValueObject.End")
        {
            RuleFor(v => v.ValueStart)
                .GreaterThanOrEqualTo(float.MinValue)
                .LessThanOrEqualTo(v => v.ValueEnd)
                .WithName(nameStart);

            RuleFor(v => v.ValueEnd)
                .GreaterThanOrEqualTo(v => v.ValueStart)
                .LessThanOrEqualTo(float.MaxValue)
                .WithName(nameEnd);
        }

        protected override void RaiseValidationException(ValidationContext<FloatRangeValueObject> context, ValidationResult result)
        {
            float Value = result.Errors.Count > 0 ? (float)result.Errors.First().AttemptedValue : 0;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new FloatRangeValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
