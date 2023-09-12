namespace Shared.Exception.Domain
{
    public abstract class IExistException : IAggregateException
    {
        protected IExistException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner) { }
        protected IExistException(Guid aggregateTypeId, string message, System.Exception? inner) : base(Guid.NewGuid(), aggregateTypeId, 409, DateTime.UtcNow, message, inner) { }
    }
}
