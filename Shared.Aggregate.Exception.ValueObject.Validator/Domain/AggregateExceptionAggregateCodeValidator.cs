using FluentValidation;
using FluentValidation.Results;
using Shared.Aggregate.Exception.ValueObject.Domain;
using Shared.ValueObject.Validator.Domain;

namespace Shared.Aggregate.Exception.ValueObject.Validator.Domain
{
    public class AggregateExceptionAggregateCodeValidator : AbstractValidator<AggregateExceptionAggregateCode>
    {
        public AggregateExceptionAggregateCodeValidator()
        {
            Include(new IntValueObjectValidator("AggregateException.AggregateCode"));

            RuleFor(v => v.Value)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(999)
                .WithName("AggregateException.AggregateCode");
        }

        protected override void RaiseValidationException(ValidationContext<AggregateExceptionAggregateCode> context, ValidationResult result)
        {
            int Value = result.Errors.Count > 0 ? (int)result.Errors.First().AttemptedValue : 0;
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new AggregateExceptionAggregateCodeConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
