namespace Shared.Bus.Query.Domain
{
    public abstract class IAggregateQuery : Message.Domain.IAggregateMessage, IQuery
    {
        public IAggregateQuery(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt) { }
        public IAggregateQuery(DateTime aggregateOccurredAt) : base(aggregateOccurredAt) { }
        public IAggregateQuery(Guid aggregateId) : base(aggregateId) { }
        public IAggregateQuery() : base() { }
    }
}
