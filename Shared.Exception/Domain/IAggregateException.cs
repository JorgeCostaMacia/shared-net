namespace Shared.Exception.Domain;

public abstract class IAggregateException : IException
{
    public Guid Id { get; init; }
    public Guid TypeId { get; init; }
    public int Code { get; init; }
    public DateTime OccurredAt { get; init; }

    protected IAggregateException(Guid id, Guid typeId, int code, DateTime occurredAt, string? message, System.Exception? inner) : base(message, inner)
    {
        Id = id;
        TypeId = typeId;
        Code = code;
        OccurredAt = occurredAt;
    }

    protected IAggregateException(Guid id, Guid typeId, int code, string message, System.Exception? inner) : this(id, typeId, code, DateTime.UtcNow, $"{id}/{typeId}: {message}", inner) { }
}