using Shared.Aggregate.Domain;

namespace Shared.Aggregate.Message.Domain
{
    public abstract class IAggregateMessage : IAggregate
    {
        public Guid AggregateId { get; }
        public DateTime AggregateOccurredAt { get; }
        public static string AggregateRoute() => throw new NotImplementedException();

        public IAggregateMessage(Guid aggregateId, DateTime aggregateOccurredAt)
        {
            AggregateId = aggregateId;
            AggregateOccurredAt = aggregateOccurredAt;
        }

        public IAggregateMessage(DateTime aggregateOccurredAt)
        {
            AggregateId = Guid.NewGuid();
            AggregateOccurredAt = aggregateOccurredAt;
        }

        public IAggregateMessage(Guid aggregateId)
        {
            AggregateId = aggregateId;
            AggregateOccurredAt = DateTime.UtcNow;
        }

        public IAggregateMessage()
        {
            AggregateId = Guid.NewGuid();
            AggregateOccurredAt = DateTime.UtcNow;
        }
    }
}
