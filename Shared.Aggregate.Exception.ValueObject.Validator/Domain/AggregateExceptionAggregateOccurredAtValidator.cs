using FluentValidation;
using FluentValidation.Results;
using Shared.Aggregate.Exception.ValueObject.Domain;
using Shared.ValueObject.Validator.Domain;

namespace Shared.Aggregate.Exception.ValueObject.Validator.Domain
{
    public class AggregateExceptionAggregateOccurredAtValidator : AbstractValidator<AggregateExceptionAggregateOccurredAt>
    {
        public AggregateExceptionAggregateOccurredAtValidator()
        {
            Include(new DateTimeValueObjectValidator("AggregateException.AggregateOccurredAt"));

            RuleFor(v => v.Value)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithName("AggregateException.AggregateOccurredAt");
        }

        protected override void RaiseValidationException(ValidationContext<AggregateExceptionAggregateOccurredAt> context, ValidationResult result) => throw new AggregateExceptionAggregateOccurredAtConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
