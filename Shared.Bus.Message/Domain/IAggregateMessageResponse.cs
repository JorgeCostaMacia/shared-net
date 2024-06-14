using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain;

public abstract class IAggregateMessageResponse : IMessageResponse, IAggregate
{
    public Guid AggregateId { get; init; }
    public string AggregateName { get; init; }
    public DateTime AggregateOccurredAt { get; init; }

    protected IAggregateMessageResponse(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt)
    {
        AggregateId = aggregateId;
        AggregateName = aggregateName;
        AggregateOccurredAt = aggregateOccurredAt;
    }

    protected IAggregateMessageResponse(string aggregateName) : this(Guid.NewGuid(), aggregateName, DateTime.UtcNow) { }
}
