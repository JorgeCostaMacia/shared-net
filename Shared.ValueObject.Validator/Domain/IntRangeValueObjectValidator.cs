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

        protected override void RaiseValidationException(ValidationContext<IntRangeValueObject> context, ValidationResult result)
        {
            int Value = result.Errors.Count > 0 ? (int)result.Errors.First().AttemptedValue : 0;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new IntRangeValueObjectConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
