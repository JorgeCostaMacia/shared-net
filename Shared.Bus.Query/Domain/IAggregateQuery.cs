namespace Shared.Bus.Query.Domain;

public abstract class IAggregateQuery : Message.Domain.IAggregateMessage, IQuery
{
    protected IAggregateQuery(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt) { }
    protected IAggregateQuery() : base() { }
}
