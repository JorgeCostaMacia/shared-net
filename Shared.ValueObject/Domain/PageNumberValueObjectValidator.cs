using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageNumberValueObjectValidator : AbstractValidator<PageNumberValueObject>, Shared.Validator.Domain.IValidator
{
    public PageNumberValueObjectValidator(string name = "PageNumberValueObject")
    {
        Include(new IntValueObjectValidator(name));

        RuleFor(v => v.Value)
            .NotEmpty()
            .WithName(name);
    }

    protected override void RaiseValidationException(ValidationContext<PageNumberValueObject> context, ValidationResult result) => throw new PageNumberValueObjectConstraintException(null, result.Errors);
}
