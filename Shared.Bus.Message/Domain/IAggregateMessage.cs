using System.Collections.Immutable;

namespace Shared.Bus.Message.Domain;

public abstract record IAggregateMessage : IMessage
{
    public Guid AggregateId { get; init; }
    public Guid AggregateCorrelationId { get; init; }
    public DateTime AggregateOccurredAt { get; init; }
    public ImmutableList<string> AggregateSubscribers { get; init; }

    protected IAggregateMessage(Guid aggregateId, Guid aggregateCorrelationId, DateTime aggregateOccurredAt, ImmutableList<string> aggregateSubscribers)
    {
        AggregateId = aggregateId;
        AggregateCorrelationId = aggregateCorrelationId;
        AggregateOccurredAt = aggregateOccurredAt;
        AggregateSubscribers = aggregateSubscribers;
    }

    protected IAggregateMessage(Guid? aggregateId, Guid? aggregateCorrelationId, DateTime? aggregateOccurredAt, IEnumerable<string>? aggregateSubscribers)
    {
        AggregateId = aggregateId ?? CreateGuid();
        AggregateCorrelationId = aggregateCorrelationId ?? AggregateId;
        AggregateOccurredAt = aggregateOccurredAt ?? DateTime.UtcNow;
        AggregateSubscribers = aggregateSubscribers?.ToImmutableList() ?? ImmutableList<string>.Empty;
    }

    private static Guid CreateGuid()
    {
#if NET9_0
        return Guid.CreateVersion7();
#else
        return Guid.NewGuid();
#endif
    }
}