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

        protected override void RaiseValidationException(ValidationContext<PageNumberValueObject> context, ValidationResult result) => throw new PageNumberValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
