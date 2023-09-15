namespace Shared.Bus.Query.Domain
{
    public abstract class IAggregateQuery : Message.Domain.IAggregateMessage, IQuery
    {
        protected IAggregateQuery(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt) : base(aggregateId, aggregateName, aggregateOccurredAt) { }
        protected IAggregateQuery(string aggregateName) : base(Guid.NewGuid(), aggregateName, DateTime.UtcNow) { }
    }
}
