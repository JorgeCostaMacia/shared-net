using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class OrderTypeValueObjectValidator : AbstractValidator<OrderTypeValueObject>
    {
        public OrderTypeValueObjectValidator()
        {
            RuleFor(v => v.Value)
                .GreaterThan(0)
                .LessThan(3)
                .WithName("OrderTypeValueObject");
        }

        protected override void RaiseValidationException(ValidationContext<OrderTypeValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            int value = 0;

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (int)error.AttemptedValue;
            }

            throw new OrderTypeValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
