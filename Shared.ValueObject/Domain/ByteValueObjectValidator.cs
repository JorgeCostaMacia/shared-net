using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class ByteValueObjectValidator : AbstractValidator<ByteValueObject>, Shared.Validator.Domain.IValidator
{
    public ByteValueObjectValidator() { }

    protected override void RaiseValidationException(ValidationContext<ByteValueObject> context, ValidationResult result) => throw new ByteValueObjectConstraintException(null, result.Errors);
}
