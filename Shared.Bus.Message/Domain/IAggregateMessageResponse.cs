using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain
{
    public abstract class IAggregateMessageResponse : IMessageResponse, IAggregate
    {
        public Guid AggregateId { get; }
        public string AggregateName { get; }
        public DateTime AggregateOccurredAt { get; }

        protected IAggregateMessageResponse(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt)
        {
            AggregateId = aggregateId;
            AggregateName = aggregateName;
            AggregateOccurredAt = aggregateOccurredAt;
        }
    }
}
