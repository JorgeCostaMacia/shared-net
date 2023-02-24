using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class PageSizeValueObjectValidator : AbstractValidator<PageSizeValueObject>
    {
        public PageSizeValueObjectValidator()
        {
            RuleFor(v => v.Value)
                .GreaterThan(0)
                .LessThan(2147483647)
                .WithName("PageSizeValueObject");
        }

        protected override void RaiseValidationException(ValidationContext<PageSizeValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            int value = 0;

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (int)error.AttemptedValue;
            }

            throw new PageSizeValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
