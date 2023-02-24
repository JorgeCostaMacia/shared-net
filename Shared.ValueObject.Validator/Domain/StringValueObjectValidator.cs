using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class StringValueObjectValidator : AbstractValidator<StringValueObject>
    {
        public StringValueObjectValidator()
        {
            RuleFor(v => v.Value)
                  .Length(0, 255)
                .WithName("StringValueObject");
        }

        protected override void RaiseValidationException(ValidationContext<StringValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            string value = "";

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (string)error.AttemptedValue;
            }

            throw new StringValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
