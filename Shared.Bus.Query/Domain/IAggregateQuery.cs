using System.Collections.Immutable;

namespace Shared.Bus.Query.Domain;

public abstract record IAggregateQuery : Message.Domain.IAggregateMessage, IQuery
{
    protected IAggregateQuery(Guid aggregateId, Guid aggregateCorrelationId, DateTime aggregateOccurredAt, ImmutableList<string> aggregateSubscribers) : base(aggregateId, aggregateCorrelationId, aggregateOccurredAt, aggregateSubscribers) { }
    protected IAggregateQuery(Guid? aggregateId = null, Guid? aggregateCorrelationId = null, DateTime? aggregateOccurredAt = null, IEnumerable<string>? aggregateSubscribers = null) : base(aggregateId, aggregateCorrelationId, aggregateOccurredAt, aggregateSubscribers) { }
}
