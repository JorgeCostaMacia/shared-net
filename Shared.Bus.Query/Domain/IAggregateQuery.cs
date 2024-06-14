namespace Shared.Bus.Query.Domain;

public abstract class IAggregateQuery : Message.Domain.IAggregateMessage, IQuery
{
    protected IAggregateQuery(Guid aggregateId, string aggregateConsumer, DateTime aggregateOccurredAt) : base(aggregateId, aggregateConsumer, aggregateOccurredAt) { }
    protected IAggregateQuery(string aggregateConsumer) : base(aggregateConsumer) { }
}
