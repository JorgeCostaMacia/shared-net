namespace Shared.Exception.Domain;

public abstract class IAggregateException : IDomainException
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

    protected IAggregateException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string message, System.Exception? innerException) : base(
        (aggregateId ?? (aggregateId = CreateGuid())).ToString() +
        "/" +
        (aggregateTypeId ?? (aggregateTypeId = CreateGuid())).ToString()
        + $": {message}", innerException)
    {
        AggregateId = aggregateId ?? CreateGuid();
        AggregateTypeId = aggregateTypeId ?? CreateGuid();
        AggregateCode = aggregateCode ?? 500;
        AggregateOccurredAt = aggregateOccurredAt ?? DateTime.UtcNow;
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