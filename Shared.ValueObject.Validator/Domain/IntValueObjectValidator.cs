using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class IntValueObjectValidator : AbstractValidator<IntValueObject>, Shared.Validator.Domain.IValidator
    {
        public IntValueObjectValidator(string name = "IntValueObject")
        {
            RuleFor(v => v.Value)
                .GreaterThanOrEqualTo(int.MinValue)
                .LessThanOrEqualTo(int.MaxValue)
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<IntValueObject> context, ValidationResult result)
        {
            int Value = result.Errors.Count > 0 ? (int)result.Errors.First().AttemptedValue : 0;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new IntValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
