using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class BoolValueObjectValidator : AbstractValidator<BoolValueObject>, Shared.Validator.Domain.IValidator
    {
        public BoolValueObjectValidator(string name = "BoolValueObject")
        {
            //RuleFor(v => v.Value)
            //    .NotEmpty();
            //    .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<BoolValueObject> context, ValidationResult result) => throw new BoolValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
