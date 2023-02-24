using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class UuidValueObjectValidator : AbstractValidator<UuidValueObject>
    {
        public UuidValueObjectValidator()
        {
            //RuleFor(v => v.Value)
            //      .Length(0, 255);
        }

        protected override void RaiseValidationException(ValidationContext<UuidValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            Guid value = new Guid();

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (Guid)error.AttemptedValue;
            }

            throw new UuidValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
