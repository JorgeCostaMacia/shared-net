using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class OrderByValueObjectValidator : AbstractValidator<OrderByValueObject>, Shared.Validator.Domain.IValidator
    {
        public OrderByValueObjectValidator(string name = "OrderByValueObject")
        {
            RuleFor(v => v.Value)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(8000)
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<OrderByValueObject> context, ValidationResult result)
        {
            string Value = result.Errors.Count > 0 ? result.Errors.First().AttemptedValue.ToString() ?? String.Empty : String.Empty;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new OrderByValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
