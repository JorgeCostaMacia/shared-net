namespace Shared.Bus.Event.Domain;

public abstract class IAggregateEvent : Message.Domain.IAggregateMessage, IEvent
{
    protected IAggregateEvent(Guid aggregateId, string aggregateConsumer, Guid aggregateSubscriber, DateTime aggregateOccurredAt) : base(aggregateId, aggregateConsumer, aggregateSubscriber, aggregateOccurredAt) { }
    protected IAggregateEvent(string aggregateConsumer, Guid aggregateSubscriber) : base(aggregateConsumer, aggregateSubscriber) { }
    protected IAggregateEvent(string aggregateConsumer) : base(aggregateConsumer) { }
}
