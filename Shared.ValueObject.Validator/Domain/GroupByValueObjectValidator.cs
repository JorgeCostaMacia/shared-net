using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class GroupByValueObjectValidator : AbstractValidator<GroupByValueObject>
    {
        public GroupByValueObjectValidator()
        {
            RuleFor(v => v.Value)
                  .Length(1, 255)
                .WithName("GroupByValueObject");
        }

        protected override void RaiseValidationException(ValidationContext<GroupByValueObject> context, ValidationResult result)
        {
            List<string> errors = new List<string>();
            string value = "";

            foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
                value = (string)error.AttemptedValue;
            }

            throw new GroupByValueObjectConstraintException(value, errors, new ValidationException(result.Errors));
        }
    }
}
