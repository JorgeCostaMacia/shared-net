using System.Collections.Immutable;

namespace Shared.Bus.Event.Domain;

public abstract record IAggregateEvent : Message.Domain.IAggregateMessage, IEvent
{
    protected IAggregateEvent(Guid aggregateId, Guid aggregateCorrelationId, DateTime aggregateOccurredAt, ImmutableList<string> aggregateSubscribers) : base(aggregateId, aggregateCorrelationId, aggregateOccurredAt, aggregateSubscribers) { }
    protected IAggregateEvent(Guid? aggregateId = null, Guid? aggregateCorrelationId = null, DateTime? aggregateOccurredAt = null, IEnumerable<string>? aggregateSubscribers = null) : base(aggregateId, aggregateCorrelationId, aggregateOccurredAt, aggregateSubscribers) { }
}