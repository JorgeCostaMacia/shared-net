using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain;

public abstract class IAggregateMessageSubscriber : IMessageHandler, IAggregate
{
    public static string AggregateRoute(Version versionSubscriber, Version versionEvent) => throw new NotImplementedException();
}