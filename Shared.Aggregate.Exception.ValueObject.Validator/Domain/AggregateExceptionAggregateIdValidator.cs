using FluentValidation;
using FluentValidation.Results;
using Shared.Aggregate.Exception.ValueObject.Domain;
using Shared.ValueObject.Validator.Domain;

namespace Shared.Aggregate.Exception.ValueObject.Validator.Domain
{
    public class AggregateExceptionAggregateIdValidator : AbstractValidator<AggregateExceptionAggregateId>
    {
        public AggregateExceptionAggregateIdValidator()
        {
            Include(new UuidValueObjectValidator("AggregateException.AggregateId"));
        }

        protected override void RaiseValidationException(ValidationContext<AggregateExceptionAggregateId> context, ValidationResult result)
        {
            Guid Value = result.Errors.Count > 0 ? (Guid)result.Errors.First().AttemptedValue : new Guid();
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new AggregateExceptionAggregateIdConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
