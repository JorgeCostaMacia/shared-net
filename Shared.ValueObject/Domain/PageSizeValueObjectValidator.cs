using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageSizeValueObjectValidator : AbstractValidator<PageSizeValueObject>, Shared.Validator.Domain.IValidator
{
    public PageSizeValueObjectValidator(string name = "PageSizeValueObject")
    {
        Include(new IntValueObjectValidator(name));

        RuleFor(v => v.Value)
            .NotEmpty()
            .WithName(name);
    }

    protected override void RaiseValidationException(ValidationContext<PageSizeValueObject> context, ValidationResult result) => throw new PageSizeValueObjectConstraintException(null, result.Errors);
}
