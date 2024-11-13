using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageSizeValueObjectValidator : AbstractValidator<PageSizeValueObject>, Shared.Validator.Domain.IValidator
{
    public PageSizeValueObjectValidator(IValidator<IntValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
            .NotEmpty()
            .WithName(v => v.GetType().Name);
    }

    protected override void RaiseValidationException(ValidationContext<PageSizeValueObject> context, ValidationResult result) => throw new PageSizeValueObjectConstraintException(null, result.Errors);
}
