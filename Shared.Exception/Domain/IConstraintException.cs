using FluentValidation.Results;
using System.Collections.Immutable;

namespace Shared.Exception.Domain
{
    public abstract class IConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : IAggregateException(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
    {
        public ImmutableList<ValidationFailure> Constraints { get; init; } = constraints.ToImmutableList();

        protected IConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : this(constraints, Guid.NewGuid(), aggregateTypeId, 400, DateTime.UtcNow, $"{message} => {string.Join(",", constraints.Select(e => e.PropertyName + ": " + e.ErrorMessage))}", inner) { }
    }
}
