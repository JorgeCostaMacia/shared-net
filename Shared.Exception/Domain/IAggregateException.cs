namespace Shared.Exception.Domain;

public abstract class IAggregateException : IException
{
    public Guid AggregateId { get; init; }
    public Guid AggregateTypeId { get; init; }
    public int AggregateCode { get; init; }
    public DateTime AggregateOccurredAt { get; init; }

    protected IAggregateException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string? message, System.Exception? innerException) : base(message, innerException)
    {
        AggregateId = aggregateId;
        AggregateTypeId = aggregateTypeId;
        AggregateCode = aggregateCode;
        AggregateOccurredAt = aggregateOccurredAt;
    }

    protected IAggregateException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, string message, System.Exception? innerException) : this(aggregateId, aggregateTypeId, aggregateCode, DateTime.UtcNow, $"{aggregateId}/{aggregateTypeId}: {message}", innerException) { }
}