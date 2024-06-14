using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace Shared.ValueObject.Domain;

public class IpValueObjectValidator : AbstractValidator<IpValueObject>, Shared.Validator.Domain.IValidator
{
    public IpValueObjectValidator(string name = "IpValueObject")
    {
        Include(new StringValueObjectValidator(name));

        RuleFor(v => v.Value)
             .NotEmpty()
             .MinimumLength(7)
             .MaximumLength(15)
             .Must(v => string.IsNullOrEmpty(v) || (v.Count(c => c == '.') == 3 && IPAddress.TryParse(v, out _))).WithErrorCode("IpValidator").WithMessage("{PropertyName} must be a IP")
             .WithName(name);
    }

    protected override void RaiseValidationException(ValidationContext<IpValueObject> context, ValidationResult result) => throw new IpValueObjectConstraintException(null, result.Errors);
}
