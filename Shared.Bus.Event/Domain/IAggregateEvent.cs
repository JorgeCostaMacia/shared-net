using System.Collections.Immutable;

namespace Shared.Bus.Event.Domain;

public abstract record IAggregateEvent : Message.Domain.IAggregateMessage, IEvent
{
    public ImmutableList<string> AggregateSubscribers { get; init; }

    protected IAggregateEvent(Guid aggregateId, DateTime aggregateOccurredAt, ImmutableList<string> aggregateSubscribers) : base(aggregateId, aggregateOccurredAt) 
    {
        AggregateSubscribers = aggregateSubscribers;
    }

    protected IAggregateEvent(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt)
    {
        AggregateSubscribers = ImmutableList<string>.Empty;
    }

    protected IAggregateEvent(IEnumerable<string> aggregateConsumers) : base() 
    {
        AggregateSubscribers = aggregateConsumers.ToImmutableList();
    }

    protected IAggregateEvent() : base()
    {
        AggregateSubscribers = ImmutableList<string>.Empty;
    }
}