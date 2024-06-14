using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain;

public abstract class IAggregateMessage : IMessage, IAggregate
{
    public Guid AggregateId { get; init; }
    public string AggregateConsumer { get; init; }
    public Guid AggregateSubscriber { get; init; }
    public DateTime AggregateOccurredAt { get; init; }

    protected IAggregateMessage(Guid aggregateId, string aggregateConsumer, Guid aggregateSubscriber, DateTime aggregateOccurredAt)
    {
        AggregateId = aggregateId;
        AggregateConsumer = aggregateConsumer;
        AggregateSubscriber = aggregateSubscriber;
        AggregateOccurredAt = aggregateOccurredAt;
    }

    protected IAggregateMessage(string aggregateConsumer, Guid aggregateSubscriber) : this(Guid.NewGuid(), aggregateConsumer, aggregateSubscriber, DateTime.UtcNow) { }
    protected IAggregateMessage(string aggregateConsumer) : this(aggregateConsumer, Guid.Empty) { }

    public static string AggregateRoute() => throw new NotImplementedException();
}
