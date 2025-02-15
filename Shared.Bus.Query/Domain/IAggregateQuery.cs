using System.Collections.Immutable;

namespace Shared.Bus.Query.Domain;

public abstract record IAggregateQuery : Message.Domain.IAggregateMessage, IQuery
{
    protected IAggregateQuery(Guid aggregateId, DateTime aggregateOccurredAt, ImmutableList<string> aggregateConsumers) : base(aggregateId, aggregateOccurredAt, aggregateConsumers) { }
    protected IAggregateQuery(List<string> aggregateConsumers) : base(aggregateConsumers) { }
    protected IAggregateQuery() : base() { }
}
