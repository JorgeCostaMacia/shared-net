using FluentValidation;
using FluentValidation.Results;
using Shared.Aggregate.Message.ValueObject.Domain;
using Shared.ValueObject.Validator.Domain;

namespace Shared.Aggregate.Message.ValueObject.Validator.Domain
{
    public class AggregateMessageAggregateIdValidator : AbstractValidator<AggregateMessageAggregateId>
    {
        public AggregateMessageAggregateIdValidator()
        {
            Include(new UuidValueObjectValidator("AggregateMessage.AggregateId"));
        }

        protected override void RaiseValidationException(ValidationContext<AggregateMessageAggregateId> context, ValidationResult result)
        {
            Guid Value = result.Errors.Count > 0 ? (Guid)result.Errors.First().AttemptedValue : new Guid();
            List<string> Constraint = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new AggregateMessageAggregateIdConstraintException(Value, Constraint, new ValidationException(result.Errors));
        }
    }
}
