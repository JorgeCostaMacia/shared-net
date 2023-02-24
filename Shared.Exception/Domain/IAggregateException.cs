namespace Shared.Exception.Domain
{
    public abstract class IAggregateException : Aggregate.Exception.Domain.IAggregateException, IException
    {
        public IAggregateException(Guid aggregateId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception inner) : base(aggregateId, aggregateCode, aggregateOccurredAt, message, inner) { }
        public IAggregateException(Guid aggregateId, int aggregateCode, string message, System.Exception inner) : base(aggregateId, aggregateCode, message, inner) { }
        public IAggregateException(Guid aggregateId, int aggregateCode, string message) : base(aggregateId, aggregateCode, message) { }
        public IAggregateException(string message, System.Exception inner) : base(message, inner) { }
        public IAggregateException(string message) : base(message) { }
        public IAggregateException() : base() { }
    }
}
