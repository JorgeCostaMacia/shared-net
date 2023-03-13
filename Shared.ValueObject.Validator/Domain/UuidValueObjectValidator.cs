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

        protected override void RaiseValidationException(ValidationContext<UuidValueObject> context, ValidationResult result)
        {
            Guid Value = result.Errors.Count > 0 ? (Guid)result.Errors.First().AttemptedValue : new Guid();
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new UuidValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
