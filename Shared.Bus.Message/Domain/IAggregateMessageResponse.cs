using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain;

public abstract class IAggregateMessageResponse(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt) : IMessageResponse, IAggregate
{
    public Guid AggregateId { get; init; } = aggregateId;
    public string AggregateName { get; init; } = aggregateName;
    public DateTime AggregateOccurredAt { get; init; } = aggregateOccurredAt;
}
