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

        protected override void RaiseValidationException(ValidationContext<AggregateExceptionAggregateId> context, ValidationResult result) => throw new AggregateExceptionAggregateIdConstraintException(context.InstanceToValidate, result.Errors.Select(e => e.ErrorMessage).ToList(), new ValidationException(result.Errors));
    }
}
