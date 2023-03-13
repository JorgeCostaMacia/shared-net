using FluentValidation;
using FluentValidation.Results;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Exception.Domain;

namespace Shared.ValueObject.Validator.Domain
{
    public class IntRangeValueObjectValidator : AbstractValidator<IntRangeValueObject>, Shared.Validator.Domain.IValidator
    {
        public IntRangeValueObjectValidator(string nameStart = "IntRangeValueObject.Start", string nameEnd = "IntRangeValueObject.End")
        {
            RuleFor(v => v.ValueStart)
                .GreaterThanOrEqualTo(int.MinValue)
                .LessThanOrEqualTo(v => v.ValueEnd)
                .WithName(nameStart);

            RuleFor(v => v.ValueEnd)
                .GreaterThanOrEqualTo(v => v.ValueStart)
                .LessThanOrEqualTo(int.MaxValue)
                .WithName(nameEnd);
        }

        protected override void RaiseValidationException(ValidationContext<IntRangeValueObject> context, ValidationResult result) => throw new IntRangeValueObjectConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
