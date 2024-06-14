namespace Shared.Exception.Domain;

public abstract class INotFoundException : IAggregateException
{
    protected INotFoundException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner) { }
    protected INotFoundException(Guid aggregateTypeId, string message, System.Exception? inner) : base(Guid.NewGuid(), aggregateTypeId, 404, message, inner) { }
}

