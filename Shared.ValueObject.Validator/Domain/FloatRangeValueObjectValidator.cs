using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class FloatRangeValueObjectValidator : AbstractValidator<FloatRangeValueObject>
    {
        public FloatRangeValueObjectValidator()
        {
            RuleFor(v => v.ValueStart)
                      .GreaterThanOrEqualTo(-2147483648)
                      .LessThanOrEqualTo(v => v.ValueEnd)
                      .WithName("FloatRangeValueObject.Start");

            RuleFor(v => v.ValueEnd)
                .GreaterThanOrEqualTo(v => v.ValueStart)
                .LessThanOrEqualTo(2147483647)
                .WithName("FloatRangeValueObject.End");
        }

        protected override void RaiseValidationException(ValidationContext<FloatRangeValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            float value = 0;

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (float)error.AttemptedValue;
            }

            throw new FloatRangeValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
