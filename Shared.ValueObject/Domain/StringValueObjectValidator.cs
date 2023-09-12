using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class StringValueObjectValidator : AbstractValidator<StringValueObject>, Shared.Validator.Domain.IValidator
    {
        public StringValueObjectValidator(string name = "StringValueObject")
        {
            RuleFor(v => v.Value)
                .MinimumLength(0)
                .MaximumLength(2147483647)
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<StringValueObject> context, ValidationResult result) => throw new StringValueObjectConstraintException(result.Errors);
    }
}
