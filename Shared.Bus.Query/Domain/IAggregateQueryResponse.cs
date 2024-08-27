namespace Shared.Bus.Query.Domain;

public abstract class IAggregateQueryResponse : Message.Domain.IAggregateMessage, IQuery
{
    protected IAggregateQueryResponse(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt) { }
    protected IAggregateQueryResponse() : base() { }
}
