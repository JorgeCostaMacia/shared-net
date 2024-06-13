using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class GroupByValueObjectValidator : AbstractValidator<GroupByValueObject>, Shared.Validator.Domain.IValidator
{
    public GroupByValueObjectValidator(string name = "GroupByValueObject")
    {
        Include(new StringValueObjectValidator(name));

        RuleFor(v => v.Value)
             .NotEmpty()
             .WithName(name);
    }

    protected override void RaiseValidationException(ValidationContext<GroupByValueObject> context, ValidationResult result) => throw new GroupByValueObjectConstraintException(result.Errors);
}
