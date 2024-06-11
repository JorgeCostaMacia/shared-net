namespace Shared.Bus.Event.Domain;

public abstract class IAggregateEvent(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt) : Message.Domain.IAggregateMessage(aggregateId, aggregateName, aggregateOccurredAt), IEvent
{
    public IAggregateEvent(string aggregateName) : this(Guid.NewGuid(), aggregateName, DateTime.UtcNow) { }
}
