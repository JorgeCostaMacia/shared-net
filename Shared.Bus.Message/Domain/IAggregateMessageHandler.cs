using Shared.Aggregate.Domain;

namespace Shared.Bus.Message.Domain;

public abstract class IAggregateMessageHandler : IMessageHandler, IAggregate
{
    public static string AggregateRoute() => throw new NotImplementedException();
    public static Guid AggregateRouteId() => throw new NotImplementedException();
}