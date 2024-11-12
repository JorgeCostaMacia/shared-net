using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class BoolValueObjectValidator : AbstractValidator<BoolValueObject>, Shared.Validator.Domain.IValidator
{
    public BoolValueObjectValidator() { }

    protected override void RaiseValidationException(ValidationContext<BoolValueObject> context, ValidationResult result) => throw new BoolValueObjectConstraintException(null, result.Errors);
}