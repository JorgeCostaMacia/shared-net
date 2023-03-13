using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class PageSizeValueObjectValidator : AbstractValidator<PageSizeValueObject>, Shared.Validator.Domain.IValidator
    {
        public PageSizeValueObjectValidator(string name = "PageSizeValueObject")
        {
            RuleFor(v => v.Value)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue)
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<PageSizeValueObject> context, ValidationResult result) => throw new PageSizeValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
