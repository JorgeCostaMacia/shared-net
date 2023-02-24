using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class BoolValueObjectValidator : AbstractValidator<BoolValueObject>
    {
        public BoolValueObjectValidator()
        {
            //RuleFor(v => v.Value)
            //    .NotEmpty();
        }

        protected override void RaiseValidationException(ValidationContext<BoolValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            bool value = false;

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (bool)error.AttemptedValue;
            }

            throw new BoolValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
