using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class GroupByValueObjectValidator : AbstractValidator<GroupByValueObject>, Shared.Validator.Domain.IValidator
    {
        public GroupByValueObjectValidator(string name = "GroupByValueObject")
        {
            RuleFor(v => v.Value)
                 .NotEmpty()
                 .MinimumLength(1)
                 .MaximumLength(8000)
                 .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<GroupByValueObject> context, ValidationResult result)
        {
            string Value = result.Errors.Count > 0 ? result.Errors.First().AttemptedValue.ToString() ?? String.Empty : String.Empty;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new GroupByValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
