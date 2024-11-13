using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class DecimalValueObjectValidator : AbstractValidator<DecimalValueObject>, Shared.Validator.Domain.IValidator
{
    public DecimalValueObjectValidator() { }

    protected override void RaiseValidationException(ValidationContext<DecimalValueObject> context, ValidationResult result) => throw new DecimalValueObjectConstraintException(null, result.Errors);
}
