namespace Shared.Bus.Query.Domain;

public abstract class IAggregateQuery : Message.Domain.IAggregateMessage, IQuery
{
    protected IAggregateQuery(Guid aggregateId, string aggregateConsumer, Guid aggregateSubscriber, DateTime aggregateOccurredAt) : base(aggregateId, aggregateConsumer, aggregateSubscriber, aggregateOccurredAt) { }
    protected IAggregateQuery(string aggregateConsumer, Guid aggregateSubscriber) : base(aggregateConsumer, aggregateSubscriber) { }
    protected IAggregateQuery(string aggregateConsumer) : base(aggregateConsumer) { }
}
