namespace Shared.Exception.Domain;

public abstract class IExistException : IAggregateException
{
    protected IExistException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException) { }

#if NET9_0
    protected IExistException(Guid aggregateTypeId, string message, System.Exception? innerException) : base(Guid.CreateVersion7(), aggregateTypeId, 409, message, innerException) { }
#else
    protected IExistException(Guid aggregateTypeId, string message, System.Exception? innerException) : base(Guid.NewGuid(), aggregateTypeId, 409, message, innerException) { }
#endif
}