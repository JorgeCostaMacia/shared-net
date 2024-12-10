using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class EmailValueObjectValidator : AbstractValidator<EmailValueObject>, Shared.Validator.Domain.IValidator
{
    public EmailValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
            .NotEmpty()
            .EmailAddress();
    }

    protected override void RaiseValidationException(ValidationContext<EmailValueObject> context, ValidationResult result) => throw new EmailValueObjectConstraintException(null, result.Errors);
}
