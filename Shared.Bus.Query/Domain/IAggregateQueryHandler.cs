using Shared.Bus.Message.Domain;

namespace Shared.Bus.Query.Domain
{
    public abstract class IAggregateQueryHandler : IAggregateMessageHandler, IQueryHandler
    {
    }
}
