namespace Shared.Bus.Query.Domain;

public abstract class IAggregateQueryResponse(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt) : Message.Domain.IAggregateMessageResponse(aggregateId, aggregateName, aggregateOccurredAt), IQuery
{
    public IAggregateQueryResponse(string aggregateName) : this(Guid.NewGuid(), aggregateName, DateTime.UtcNow) { }
}
