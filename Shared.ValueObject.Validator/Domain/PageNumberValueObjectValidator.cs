using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class PageNumberValueObjectValidator : AbstractValidator<PageNumberValueObject>
    {
        public PageNumberValueObjectValidator()
        {
            RuleFor(v => v.Value)
                .GreaterThan(0)
                .LessThan(2147483647)
                .WithName("PageNumberValueObject");
        }

        protected override void RaiseValidationException(ValidationContext<PageNumberValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            int value = 0;

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (int)error.AttemptedValue;
            }

            throw new PageNumberValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
