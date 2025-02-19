using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace Shared.ValueObject.Domain;

public class IpValueObjectValidator : AbstractValidator<IpValueObject>, Shared.Validator.Domain.IValidator
{
    public IpValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
             .NotEmpty()
             .MinimumLength(7)
             .MaximumLength(15)
             .Must(v => string.IsNullOrEmpty(v) || (v.Count(c => c == '.') == 3 && IPAddress.TryParse(v, out _))).WithErrorCode("IpValidator").WithMessage("{PropertyName} must be a IP");
    }

    protected override void RaiseValidationException(ValidationContext<IpValueObject> context, ValidationResult result) => throw new IpValueObjectConstraintException(null, null, null, null, null, null, result.Errors);
}