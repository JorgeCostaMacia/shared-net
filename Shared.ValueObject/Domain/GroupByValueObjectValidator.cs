using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class GroupByValueObjectValidator : AbstractValidator<GroupByValueObject>, Shared.Validator.Domain.IValidator
    {
        public GroupByValueObjectValidator(string name = "GroupByValueObject")
        {
            Include(new StringValueObjectValidator(name));

            RuleFor(v => v.Value)
                 .NotEmpty()
                 .MinimumLength(1)
                 .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<GroupByValueObject> context, ValidationResult result) => throw new GroupByValueObjectConstraintException(result.Errors);
    }
}
