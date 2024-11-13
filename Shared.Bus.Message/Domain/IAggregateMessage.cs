namespace Shared.Bus.Message.Domain;

public abstract record IAggregateMessage : IMessage
{
    public Guid AggregateId { get; init; }
    public DateTime AggregateOccurredAt { get; init; }

    protected IAggregateMessage(Guid aggregateId, DateTime aggregateOccurredAt)
    {
        AggregateId = aggregateId;
        AggregateOccurredAt = aggregateOccurredAt;
    }

    protected IAggregateMessage() : this(Guid.CreateVersion7(), DateTime.UtcNow) { }
}