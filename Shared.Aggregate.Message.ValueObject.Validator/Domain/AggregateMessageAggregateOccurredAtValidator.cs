using FluentValidation;
using FluentValidation.Results;
using Shared.Aggregate.Message.ValueObject.Domain;
using Shared.ValueObject.Validator.Domain;

namespace Shared.Aggregate.Message.ValueObject.Validator.Domain
{
    public class AggregateMessageAggregateOccurredAtValidator : AbstractValidator<AggregateMessageAggregateOccurredAt>
    {
        public AggregateMessageAggregateOccurredAtValidator()
        {
            Include(new DateTimeValueObjectValidator("AggregateMessage.AggregateOccurredAt"));

            RuleFor(v => v.Value)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithName("AggregateMessage.AggregateOccurredAt");
        }

        protected override void RaiseValidationException(ValidationContext<AggregateMessageAggregateOccurredAt> context, ValidationResult result) => throw new AggregateMessageAggregateOccurredAtConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
