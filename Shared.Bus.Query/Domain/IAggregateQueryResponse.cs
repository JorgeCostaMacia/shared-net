namespace Shared.Bus.Query.Domain
{
    public abstract class IAggregateQueryResponse : Message.Domain.IAggregateMessageResponse, IQuery
    {
        protected IAggregateQueryResponse(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt) : base(aggregateId, aggregateName, aggregateOccurredAt) { }
        protected IAggregateQueryResponse(string aggregateName) : base(Guid.NewGuid(), aggregateName, DateTime.UtcNow) { }
    }
}
