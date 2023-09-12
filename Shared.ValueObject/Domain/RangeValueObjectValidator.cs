using FluentValidation;
using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class RangeValueObjectValidator<T> : AbstractValidator<RangeValueObject<T>>, Shared.Validator.Domain.IValidator
    {
        public RangeValueObjectValidator(string nameStart = "RangeValueObject.Start", string nameEnd = "RangeValueObject.End")
        {
            //RuleFor(v => v.Value)
            //    .NotEmpty();
            //    .WithName(name);
        }

        protected override void RaiseValidationException(ValidationContext<RangeValueObject<T>> context, ValidationResult result) => throw new RangeValueObjectConstraintException(result.Errors);
    }
}