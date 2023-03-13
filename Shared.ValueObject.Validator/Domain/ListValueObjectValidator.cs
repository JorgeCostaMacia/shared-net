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

        protected override void RaiseValidationException(ValidationContext<ListValueObject<T>> context, ValidationResult result)
        {
            List<T> Value = result.Errors.Count > 0 ? (List<T>)result.Errors.First().AttemptedValue : new List<T>();
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ListValueObjectConstraintException<T>(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
