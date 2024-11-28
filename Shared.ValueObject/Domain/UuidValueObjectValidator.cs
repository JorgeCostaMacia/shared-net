using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class UuidValueObjectValidator : AbstractValidator<UuidValueObject>, Validator.Domain.IValidator
{
    public UuidValueObjectValidator()
    {
        RuleFor(v => v.Value)
            .NotEmpty()
            .WithName(v => v.GetType().FullName);
    }

    protected override void RaiseValidationException(ValidationContext<UuidValueObject> context, ValidationResult result) => throw new UuidValueObjectConstraintException(null, result.Errors);
}