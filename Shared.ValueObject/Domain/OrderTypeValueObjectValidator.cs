using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class OrderTypeValueObjectValidator : AbstractValidator<OrderTypeValueObject>, Shared.Validator.Domain.IValidator
    {
        public OrderTypeValueObjectValidator(string name = "OrderTypeValueObject")
        {
            Include(new StringValueObjectValidator(name));

            RuleFor(v => v.Value)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(3)
                .Must(v2 => v2 == "ASC" || v2 == "DESC")
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<OrderTypeValueObject> context, ValidationResult result) => throw new OrderTypeValueObjectConstraintException(result.Errors);
    }
}
