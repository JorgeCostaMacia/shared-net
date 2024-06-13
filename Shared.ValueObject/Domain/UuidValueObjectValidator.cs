using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class UuidValueObjectValidator : AbstractValidator<UuidValueObject>, Validator.Domain.IValidator
{
    public UuidValueObjectValidator(string name = "UuidValueObject")
    {
        RuleFor(v => v.Value)
            .NotEmpty()
            .WithName(name);
    }

    protected override void RaiseValidationException(ValidationContext<UuidValueObject> context, ValidationResult result) => throw new UuidValueObjectConstraintException(result.Errors);
}
