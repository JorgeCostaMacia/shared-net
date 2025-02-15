using System.Collections.Immutable;

namespace Shared.Bus.Event.Domain;

public abstract record IAggregateEvent : Message.Domain.IAggregateMessage, IEvent
{
    protected IAggregateEvent(Guid aggregateId, DateTime aggregateOccurredAt, ImmutableList<string> aggregateConsumers) : base(aggregateId, aggregateOccurredAt, aggregateConsumers) { }
    protected IAggregateEvent(IEnumerable<string> aggregateConsumers) : base(aggregateConsumers) { }
    protected IAggregateEvent() : base() { }
}