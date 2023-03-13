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

        protected override void RaiseValidationException(ValidationContext<AggregateMessageAggregateId> context, ValidationResult result) => throw new AggregateMessageAggregateIdConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
