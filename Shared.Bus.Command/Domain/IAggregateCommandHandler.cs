using Shared.Bus.Message.Domain;

namespace Shared.Bus.Command.Domain;

public abstract class IAggregateCommandHandler : IAggregateMessageHandler, ICommandHandler
{
}
