using System.Collections.Immutable;

namespace Shared.Bus.Message.Domain;

public abstract record IAggregateMessage : IMessage
{
    public Guid AggregateId { get; init; }
    public DateTime AggregateOccurredAt { get; init; }
    public ImmutableList<string> AggregateConsumers { get; init; }
    public Guid CorrelationId => AggregateId;

    protected IAggregateMessage(Guid aggregateId, DateTime aggregateOccurredAt, ImmutableList<string> aggregateConsumers)
    {
        AggregateId = aggregateId;
        AggregateOccurredAt = aggregateOccurredAt;
        AggregateConsumers = aggregateConsumers;
    }

    protected IAggregateMessage() : this(
#if NET9_0
        Guid.CreateVersion7()
#else
Guid.NewGuid()
#endif
, DateTime.UtcNow, ImmutableList<string>.Empty)
    { }

    protected IAggregateMessage(IEnumerable<string> aggregateConsumers) : this(
#if NET9_0
        Guid.CreateVersion7()
#else
        Guid.NewGuid()
#endif
        , DateTime.UtcNow, aggregateConsumers.ToImmutableList())
    { }
}