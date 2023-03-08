namespace Shared.Bus.Message.Domain
{
    public abstract class IAggregateMessage : Aggregate.Message.Domain.IAggregateMessage, IMessage
    {
        public IAggregateMessage(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt) { }
        public IAggregateMessage(DateTime aggregateOccurredAt) : base(aggregateOccurredAt) { }
        public IAggregateMessage(Guid aggregateId) : base(aggregateId) { }
        public IAggregateMessage() : base() { }
    }
}
