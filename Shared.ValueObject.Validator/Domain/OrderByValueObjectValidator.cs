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

        protected override void RaiseValidationException(ValidationContext<OrderByValueObject> context, ValidationResult result) => throw new OrderByValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
