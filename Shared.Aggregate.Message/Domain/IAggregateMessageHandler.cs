using Shared.Aggregate.Domain;

namespace Shared.Aggregate.Message.Domain
{
    public interface IAggregateMessageHandler : IAggregate
    {
        public static string AggregateRoute() => throw new NotImplementedException();
    }
}
