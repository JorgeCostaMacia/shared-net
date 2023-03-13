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

        protected override void RaiseValidationException(ValidationContext<StringValueObject> context, ValidationResult result) => throw new StringValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
