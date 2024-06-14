using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain;

public abstract class IAggregateMessage : IMessage, IAggregate
{
    public Guid AggregateId { get; init; }
    public string AggregateName { get; init; }
    public DateTime AggregateOccurredAt { get; init; }

    protected IAggregateMessage(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt)
    {
        AggregateId = aggregateId;
        AggregateName = aggregateName;
        AggregateOccurredAt = aggregateOccurredAt;
    }

    protected IAggregateMessage(string aggregateName) : this(Guid.NewGuid(), aggregateName, DateTime.UtcNow) { }

    public static string AggregateRoute() => throw new NotImplementedException();
}
