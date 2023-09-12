using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;

namespace Shared.ValueObject.Domain
{
    public class JsonValueObjectValidator : AbstractValidator<JsonValueObject>, Shared.Validator.Domain.IValidator
    {
        public JsonValueObjectValidator(string name = "JsonValueObject")
        {
            Include(new StringValueObjectValidator(name));

            RuleFor(v => v.Value)
                 .NotEmpty()
                 .MinimumLength(2)
                 .Must(v => string.IsNullOrEmpty(v) || Regex.IsMatch(v, @"^\s*(\{.*\}|\[.*\])\s*$")).WithErrorCode("JsonValidator").WithMessage("{PropertyName} must be a JSON")
                 .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<JsonValueObject> context, ValidationResult result) => throw new JsonValueObjectConstraintException(result.Errors);
    }
}
