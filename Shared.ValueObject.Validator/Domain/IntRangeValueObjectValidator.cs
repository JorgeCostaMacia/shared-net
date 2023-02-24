using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class IntRangeValueObjectValidator : AbstractValidator<IntRangeValueObject>
    {
        public IntRangeValueObjectValidator()
        {
            RuleFor(v => v.ValueStart)
                .GreaterThanOrEqualTo(-2147483648)
                .LessThanOrEqualTo(v => v.ValueEnd)
                .WithName("IntRangeValueObject.Start");

            RuleFor(v => v.ValueEnd)
                .GreaterThanOrEqualTo(v => v.ValueStart)
                .LessThanOrEqualTo(2147483647)
                .WithName("IntRangeValueObject.End");
        }

        protected override void RaiseValidationException(ValidationContext<IntRangeValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            int value = 0;

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (int)error.AttemptedValue;
            }

            throw new IntRangeValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
