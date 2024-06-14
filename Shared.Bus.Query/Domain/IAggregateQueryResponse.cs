namespace Shared.Bus.Query.Domain;

public abstract class IAggregateQueryResponse : Message.Domain.IAggregateMessage, IQuery
{
    protected IAggregateQueryResponse(Guid aggregateId, string aggregateConsumer, Guid aggregateSubscriber, DateTime aggregateOccurredAt) : base(aggregateId, aggregateConsumer, aggregateSubscriber, aggregateOccurredAt) { }
    protected IAggregateQueryResponse(string aggregateConsumer, Guid aggregateSubscriber) : base(aggregateConsumer, aggregateSubscriber) { }
    protected IAggregateQueryResponse(string aggregateConsumer) : base(aggregateConsumer) { }
}
