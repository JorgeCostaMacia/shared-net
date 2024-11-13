namespace Shared.Exception.Domain;

public abstract class INotFoundException : IAggregateException
{
    protected INotFoundException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException) { }
    protected INotFoundException(Guid aggregateTypeId, string message, System.Exception? innerException) : base(Guid.CreateVersion7(), aggregateTypeId, 404, message, innerException) { }
}