using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class OrderTypeValueObjectValidator : AbstractValidator<OrderTypeValueObject>, Shared.Validator.Domain.IValidator
    {
        public OrderTypeValueObjectValidator(string name = "OrderTypeValueObject")
        {
            RuleFor(v => v.Value)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue)
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<OrderTypeValueObject> context, ValidationResult result)
        {
            int Value = result.Errors.Count > 0 ? (int)result.Errors.First().AttemptedValue : 0;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new OrderTypeValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
