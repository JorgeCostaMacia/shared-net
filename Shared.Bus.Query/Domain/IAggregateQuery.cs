namespace Shared.Bus.Query.Domain;

public abstract class IAggregateQuery(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt) : Message.Domain.IAggregateMessage(aggregateId, aggregateName, aggregateOccurredAt), IQuery
{
    public IAggregateQuery(string aggregateName) : this(Guid.NewGuid(), aggregateName, DateTime.UtcNow) { }
}
