using System.Collections.Immutable;

namespace Shared.Bus.Query.Domain;

public abstract record IAggregateQuery : Message.Domain.IAggregateMessage, IQuery
{
    protected IAggregateQuery(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt) { }
    protected IAggregateQuery() : base() { }
}
