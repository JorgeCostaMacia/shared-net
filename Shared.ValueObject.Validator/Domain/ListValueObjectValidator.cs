using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class ListValueObjectValidator<T> : AbstractValidator<ListValueObject<T>>
    {
        public ListValueObjectValidator()
        {
            //RuleFor(v => v.Value)
            //    .NotEmpty();
        }

        protected override void RaiseValidationException(ValidationContext<ListValueObject<T>> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            List<T> value = new List<T>();

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (List<T>)error.AttemptedValue;
            }

            throw new ListValueObjectConstraintException<T>(value, errors, new ValidationException(result.Errors));
        }
    }
}
