using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class IntValueObjectValidator : AbstractValidator<IntValueObject>
    {
        public IntValueObjectValidator()
        {
            RuleFor(v => v.Value)
                .GreaterThan(-2147483648)
                .LessThan(2147483647)
                .WithName("IntValueObject");
        }

        protected override void RaiseValidationException(ValidationContext<IntValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            int value = 0;

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (int)error.AttemptedValue;
            }

            throw new IntValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
