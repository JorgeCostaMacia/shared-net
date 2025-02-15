using System.Collections.Immutable;

namespace Shared.Bus.Query.Domain;

public abstract record IAggregateQueryResponse : Message.Domain.IAggregateMessage, IQuery
{
    protected IAggregateQueryResponse(Guid aggregateId, DateTime aggregateOccurredAt, ImmutableList<string> aggregateConsumers) : base(aggregateId, aggregateOccurredAt, aggregateConsumers) { }
    protected IAggregateQueryResponse(IEnumerable<string> aggregateConsumers) : base(aggregateConsumers) { }
    protected IAggregateQueryResponse() : base() { }
}
