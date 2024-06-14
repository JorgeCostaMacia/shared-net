using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain;

public abstract class IAggregateMessage : IMessage, IAggregate
{
    public Guid AggregateId { get; init; }
    public string AggregateConsumer { get; init; }
    public DateTime AggregateOccurredAt { get; init; }

    protected IAggregateMessage(Guid aggregateId, string aggregateConsumer, DateTime aggregateOccurredAt)
    {
        AggregateId = aggregateId;
        AggregateConsumer = aggregateConsumer;
        AggregateOccurredAt = aggregateOccurredAt;
    }

    protected IAggregateMessage(string aggregateConsumer) : this(Guid.NewGuid(), aggregateConsumer, DateTime.UtcNow) { }

    public static string AggregateRoute() => throw new NotImplementedException();
}
