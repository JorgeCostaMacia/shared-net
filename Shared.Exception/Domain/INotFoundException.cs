namespace Shared.Exception.Domain;

public abstract class INotFoundException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : IAggregateException(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
{
    protected INotFoundException(Guid aggregateTypeId, string message, System.Exception? inner) : this(Guid.NewGuid(), aggregateTypeId, 404, DateTime.UtcNow, message, inner) { }
}

