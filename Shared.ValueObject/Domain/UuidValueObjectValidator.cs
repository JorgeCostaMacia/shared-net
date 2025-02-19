using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class UuidValueObjectValidator : AbstractValidator<UuidValueObject>, Validator.Domain.IValidator
{
    public UuidValueObjectValidator()
    {
        RuleFor(v => v.Value)
            .NotEmpty();
    }

    protected override void RaiseValidationException(ValidationContext<UuidValueObject> context, ValidationResult result) => throw new UuidValueObjectConstraintException(null, null, null, null, null, null, result.Errors);
}