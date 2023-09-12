namespace Shared.Exception.Domain
{
    public abstract class INotUpdateException : IAggregateException
    {
        protected INotUpdateException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner) { }
        protected INotUpdateException(Guid aggregateTypeId, string message, System.Exception? inner) : base(Guid.NewGuid(), aggregateTypeId, 409, DateTime.UtcNow, message, inner) { }
    }
}
