namespace Shared.Bus.Query.Domain;

public abstract class IAggregateQueryResponse : Message.Domain.IAggregateMessage, IQuery
{
    protected IAggregateQueryResponse(Guid aggregateId, string aggregateConsumer, DateTime aggregateOccurredAt) : base(aggregateId, aggregateConsumer, aggregateOccurredAt) { }
    protected IAggregateQueryResponse(string aggregateConsumer) : base(aggregateConsumer) { }
}
