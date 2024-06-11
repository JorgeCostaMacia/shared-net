namespace Shared.Exception.Domain;

public abstract class IExistException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : IAggregateException(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
{
    public IExistException(Guid aggregateTypeId, string message, System.Exception? inner) : this(Guid.NewGuid(), aggregateTypeId, 409, DateTime.UtcNow, message, inner) { }
}
