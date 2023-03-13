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

        protected override void RaiseValidationException(ValidationContext<BoolValueObject> context, ValidationResult result)
        {
            bool Value = result.Errors.Count > 0 ? (bool)result.Errors.First().AttemptedValue : false;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new BoolValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
