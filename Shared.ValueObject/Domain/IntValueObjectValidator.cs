using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class IntValueObjectValidator : AbstractValidator<IntValueObject>, Shared.Validator.Domain.IValidator
{
    public IntValueObjectValidator() { }

    protected override void RaiseValidationException(ValidationContext<IntValueObject> context, ValidationResult result) => throw new IntValueObjectConstraintException(null, null, null, null, null, null, result.Errors);
}
