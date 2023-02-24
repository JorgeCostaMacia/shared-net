using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class OrderByValueObjectValidator : AbstractValidator<OrderByValueObject>
    {
        public OrderByValueObjectValidator()
        {
            RuleFor(v => v.Value)
                  .Length(1, 255)
                .WithName("OrderByValueObject");
        }

        protected override void RaiseValidationException(ValidationContext<OrderByValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            string value = "";

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (string)error.AttemptedValue;
            }

            throw new OrderByValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
