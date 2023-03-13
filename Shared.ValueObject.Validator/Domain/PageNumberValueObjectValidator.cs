using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class PageNumberValueObjectValidator : AbstractValidator<PageNumberValueObject>, Shared.Validator.Domain.IValidator
    {
        public PageNumberValueObjectValidator(string name = "PageNumberValueObject")
        {
            RuleFor(v => v.Value)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue)
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<PageNumberValueObject> context, ValidationResult result)
        {
            int Value = result.Errors.Count > 0 ? (int)result.Errors.First().AttemptedValue : 0;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new PageNumberValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
