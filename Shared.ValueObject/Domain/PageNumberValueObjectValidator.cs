using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageNumberValueObjectValidator : AbstractValidator<PageNumberValueObject>, Shared.Validator.Domain.IValidator
{
    public PageNumberValueObjectValidator(IValidator<IntValueObject> validator)
    {
        Include(validator);

        RuleFor(v => v.Value)
            .NotEmpty()
            .WithName(v => v.GetType().Name);
    }

    protected override void RaiseValidationException(ValidationContext<PageNumberValueObject> context, ValidationResult result) => throw new PageNumberValueObjectConstraintException(null, result.Errors);
}
