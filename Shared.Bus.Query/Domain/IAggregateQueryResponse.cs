using System.Collections.Immutable;

namespace Shared.Bus.Query.Domain;

public abstract record IAggregateQueryResponse : Message.Domain.IAggregateMessage, IQuery
{
    protected IAggregateQueryResponse(Guid aggregateId, Guid aggregateCorrelationId, DateTime aggregateOccurredAt, ImmutableList<string> aggregateSubscribers) : base(aggregateId, aggregateCorrelationId, aggregateOccurredAt, aggregateSubscribers) { }
    protected IAggregateQueryResponse(Guid? aggregateId = null, Guid? aggregateCorrelationId = null, DateTime? aggregateOccurredAt = null, IEnumerable<string>? aggregateSubscribers = null) : base(aggregateId, aggregateCorrelationId, aggregateOccurredAt, aggregateSubscribers) { }
}
