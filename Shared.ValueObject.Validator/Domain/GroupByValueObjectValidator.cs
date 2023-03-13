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

        protected override void RaiseValidationException(ValidationContext<GroupByValueObject> context, ValidationResult result) => throw new GroupByValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
