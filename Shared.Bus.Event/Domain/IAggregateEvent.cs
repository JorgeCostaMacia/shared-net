namespace Shared.Bus.Event.Domain;

public abstract class IAggregateEvent : Message.Domain.IAggregateMessage, IEvent
{
    protected IAggregateEvent(Guid aggregateId, string aggregateConsumer, DateTime aggregateOccurredAt) : base(aggregateId, aggregateConsumer, aggregateOccurredAt) { }
    protected IAggregateEvent(string aggregateConsumer) : base(aggregateConsumer) { }
}
