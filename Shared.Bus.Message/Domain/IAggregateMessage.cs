namespace Shared.Bus.Message.Domain
{
    public abstract class IAggregateMessage : Aggregate.Message.Domain.IAggregateMessage, IMessage
    {
        public IAggregateMessage(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt) { }
        public IAggregateMessage() : base() { }
    }
}
