namespace Shared.Bus.Message.Domain
{
    public abstract class IAggregateMessageHandler : Aggregate.Message.Domain.IAggregateMessageHandler, IMessageHandler
    {
    }
}