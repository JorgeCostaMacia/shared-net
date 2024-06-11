using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain;

public abstract class IAggregateMessageHandler : IMessageHandler, IAggregate
{
    public static string AggregateRoute(Version version) => throw new NotImplementedException();
}