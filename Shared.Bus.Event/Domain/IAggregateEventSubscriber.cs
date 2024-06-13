using Shared.Bus.Message.Domain;

namespace Shared.Bus.Event.Domain;

public abstract class IAggregateEventSubscriber : IAggregateMessageHandler, IEventSubscriber
{
}
