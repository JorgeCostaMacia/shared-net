using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class OrderByValueObjectValidator : AbstractValidator<OrderByValueObject>, Shared.Validator.Domain.IValidator
    {
        public OrderByValueObjectValidator(string name = "OrderByValueObject")
        {
            Include(new StringValueObjectValidator(name));

            RuleFor(v => v.Value)
                 .NotEmpty()
                 .MinimumLength(1)
                 .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<OrderByValueObject> context, ValidationResult result) => throw new OrderByValueObjectConstraintException(result.Errors);
    }
}
