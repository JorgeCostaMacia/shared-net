namespace Shared.Exception.Domain;

public abstract class IExistException : IAggregateException
{
    protected IExistException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException) { }

    protected IExistException(Guid aggregateTypeId, string message, System.Exception? innerException) : base(
#if NET9_0
        Guid.CreateVersion7()
#else
            Guid.NewGuid()
#endif
        , aggregateTypeId, 409, message, innerException)
    { }
}