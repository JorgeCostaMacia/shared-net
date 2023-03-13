using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class StringValueObjectValidator : AbstractValidator<StringValueObject>, Shared.Validator.Domain.IValidator
    {
        public StringValueObjectValidator(string name = "StringValueObject")
        {
            RuleFor(v => v.Value)
                .MinimumLength(0)
                .MaximumLength(8000)
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<StringValueObject> context, ValidationResult result)
        {
            string Value = result.Errors.Count > 0 ? result.Errors.First().AttemptedValue.ToString() ?? String.Empty : String.Empty;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new StringValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
