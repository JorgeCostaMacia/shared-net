using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class OrderByValueObjectValidator : AbstractValidator<OrderByValueObject>, Shared.Validator.Domain.IValidator
{
    public OrderByValueObjectValidator(string name = "OrderByValueObject")
    {
        Include(new StringValueObjectValidator(name));

        RuleFor(v => v.Value)
             .NotEmpty()
             .WithName(name);
    }

    protected override void RaiseValidationException(ValidationContext<OrderByValueObject> context, ValidationResult result) => throw new OrderByValueObjectConstraintException(null, result.Errors);
}
