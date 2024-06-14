namespace Shared.Bus.Event.Domain;

public abstract class IAggregateEvent : Message.Domain.IAggregateMessage, IEvent
{
    public Guid AggregateDestinationId { get; init; }

    protected IAggregateEvent(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt, Guid aggregateDestinationId) : base(aggregateId, aggregateName, aggregateOccurredAt)
    {
        AggregateDestinationId = aggregateDestinationId;
    }

    protected IAggregateEvent(string aggregateName, Guid aggregateDestinationId) : base(aggregateName)
    {
        AggregateDestinationId = aggregateDestinationId;
    }

    protected IAggregateEvent(string aggregateName) : base(aggregateName) 
    {
        AggregateDestinationId = Guid.Empty;
    }
}
