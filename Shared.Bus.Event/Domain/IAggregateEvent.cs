namespace Shared.Bus.Event.Domain
{
    public abstract class IAggregateEvent : Message.Domain.IAggregateMessage, IEvent
    {
        protected IAggregateEvent(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt) : base(aggregateId, aggregateName, aggregateOccurredAt) { }
        protected IAggregateEvent(string aggregateName) : base(Guid.NewGuid(), aggregateName, DateTime.UtcNow) { }
    }
}
