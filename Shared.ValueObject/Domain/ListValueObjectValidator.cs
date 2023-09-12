using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class ListValueObjectValidator<T> : AbstractValidator<ListValueObject<T>>, Shared.Validator.Domain.IValidator
    {
        public ListValueObjectValidator(string name = "ListValueObject")
        {
            //RuleFor(v => v.Value)
            //    .NotEmpty()
            //    .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<ListValueObject<T>> context, ValidationResult result) => throw new ListValueObjectConstraintException(result.Errors);
    }
}
