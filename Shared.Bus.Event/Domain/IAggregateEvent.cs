namespace Shared.Bus.Event.Domain;

public abstract class IAggregateEvent : Message.Domain.IAggregateMessage, IEvent
{
    protected IAggregateEvent(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt) { }
    protected IAggregateEvent() : base() { }
}