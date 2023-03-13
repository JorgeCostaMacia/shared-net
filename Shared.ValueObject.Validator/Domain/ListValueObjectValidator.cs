using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class ListValueObjectValidator<T> : AbstractValidator<ListValueObject<T>>, Shared.Validator.Domain.IValidator
    {
        public ListValueObjectValidator(string name = "ListValueObject")
        {
            //RuleFor(v => v.Value)
            //    .NotEmpty()
            //    .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<ListValueObject<T>> context, ValidationResult result) => throw new ListValueObjectConstraintException<T>(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
