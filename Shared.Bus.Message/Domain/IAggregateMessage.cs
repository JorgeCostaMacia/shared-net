using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain;

public abstract class IAggregateMessage(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt) : IMessage, IAggregate
{
    public Guid AggregateId { get; init; } = aggregateId;
    public string AggregateName { get; init; } = aggregateName;
    public DateTime AggregateOccurredAt { get; init; } = aggregateOccurredAt;

    public static string AggregateRoute(Version version) => throw new NotImplementedException();
}
