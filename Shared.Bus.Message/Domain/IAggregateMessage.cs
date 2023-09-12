using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain
{
    public abstract class IAggregateMessage : IMessage, IAggregate
    {
        public Guid AggregateId { get; }
        public DateTime AggregateOccurredAt { get; }

        protected IAggregateMessage(Guid aggregateId, DateTime aggregateOccurredAt)
        {
            AggregateId = aggregateId;
            AggregateOccurredAt = aggregateOccurredAt;
        }
        public static string AggregateRoute() => throw new NotImplementedException();
    }
}
