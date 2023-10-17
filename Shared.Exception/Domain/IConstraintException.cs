using FluentValidation.Results;
using System.Collections.Immutable;

namespace Shared.Exception.Domain
{
    public abstract class IConstraintException : IAggregateException
    {
        public ImmutableList<ValidationFailure> Constraints { get; init; }

        protected IConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
            Constraints = constraints.ToImmutableList();
        }

        protected IConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(Guid.NewGuid(), aggregateTypeId, 400, DateTime.UtcNow, $"{message} => {string.Join(",", constraints.Select(failure => failure.PropertyName + ": " + failure.ErrorMessage))}", inner)
        {
            Constraints = constraints.ToImmutableList();
        }
    }
}
