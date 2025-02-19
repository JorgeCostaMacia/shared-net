using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class GroupByValueObjectValidator : AbstractValidator<GroupByValueObject>, Shared.Validator.Domain.IValidator
{
    public GroupByValueObjectValidator(IValidator<StringValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
             .NotEmpty();
    }

    protected override void RaiseValidationException(ValidationContext<GroupByValueObject> context, ValidationResult result) => throw new GroupByValueObjectConstraintException(null, null, null, null, null, null, result.Errors);
}
