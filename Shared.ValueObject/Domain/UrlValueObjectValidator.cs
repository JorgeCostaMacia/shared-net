using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class UrlValueObjectValidator : AbstractValidator<UrlValueObject>, Shared.Validator.Domain.IValidator
{
    public UrlValueObjectValidator(string name = "UrlValueObject")
    {
        Include(new StringValueObjectValidator(name));

        RuleFor(v => v.Value)
            .NotEmpty()
            .MinimumLength(1)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithErrorCode("UrlValidator").WithMessage("{PropertyName} must be an Url")
            .WithName(name);
    }

    protected override void RaiseValidationException(ValidationContext<UrlValueObject> context, ValidationResult result) => throw new UrlValueObjectConstraintException(null, result.Errors);
}