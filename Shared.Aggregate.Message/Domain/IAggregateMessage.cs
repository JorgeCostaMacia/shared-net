using Shared.Aggregate.Domain;

namespace Shared.Aggregate.Message.Domain
{
    public interface IAggregateMessage : IAggregate
    {
        public Guid AggregateId { get; }
        public DateTime AggregateOccurredAt { get; }
        public static string AggregateRoute() => throw new NotImplementedException();
    }
}
