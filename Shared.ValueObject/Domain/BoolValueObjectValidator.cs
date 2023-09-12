using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class BoolValueObjectValidator : AbstractValidator<BoolValueObject>, Shared.Validator.Domain.IValidator
    {
        public BoolValueObjectValidator(string name = "BoolValueObject")
        {
            //RuleFor(v => v.Value)
            //    .NotEmpty();
            //    .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<BoolValueObject> context, ValidationResult result) => throw new BoolValueObjectConstraintException(result.Errors);
    }
}
