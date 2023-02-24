using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class GroupTypeValueObjectValidator : AbstractValidator<GroupTypeValueObject>
    {
        public GroupTypeValueObjectValidator()
        {
            RuleFor(v => v.Value)
                .GreaterThan(0)
                .LessThan(3)
                .WithName("GroupTypeValueObject");
        }

        protected override void RaiseValidationException(ValidationContext<GroupTypeValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            int value = 0;

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (int)error.AttemptedValue;
            }

            throw new GroupTypeValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
