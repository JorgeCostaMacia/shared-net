using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class UuidValueObjectValidator : AbstractValidator<UuidValueObject>, Shared.Validator.Domain.IValidator
    {
        public UuidValueObjectValidator(string name = "UuidValueObject")
        {
            RuleFor(v => v.Value)
                .NotEmpty()
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<UuidValueObject> context, ValidationResult result) => throw new UuidValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
