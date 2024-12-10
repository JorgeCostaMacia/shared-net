using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;

namespace Shared.ValueObject.Domain;

public class JsonValueObjectValidator : AbstractValidator<JsonValueObject>, Shared.Validator.Domain.IValidator
{
    public JsonValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
             .NotEmpty()
             .MinimumLength(2)
             .Must(v => Regex.IsMatch(v, @"^\s*(\{.*\}|\[.*\])\s*$")).WithErrorCode("JsonValidator").WithMessage("{PropertyName} must be a JSON");
    }

    protected override void RaiseValidationException(ValidationContext<JsonValueObject> context, ValidationResult result) => throw new JsonValueObjectConstraintException(null, result.Errors);
}
