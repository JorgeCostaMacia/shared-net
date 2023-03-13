using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class GroupTypeValueObjectValidator : AbstractValidator<GroupTypeValueObject>, Shared.Validator.Domain.IValidator
    {
        public GroupTypeValueObjectValidator(string name = "GroupTypeValueObject")
        {
            RuleFor(v => v.Value)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(int.MaxValue)
                .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<GroupTypeValueObject> context, ValidationResult result) => throw new GroupTypeValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
