namespace Shared.Bus.Event.Domain
{
    public abstract class IAggregateEvent : Message.Domain.IAggregateMessage, IEvent
    {
        public IAggregateEvent(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt) { }
        public IAggregateEvent() : base() { }
    }
}
