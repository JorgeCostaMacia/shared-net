namespace Shared.Bus.Command.Domain
{
    public abstract class IAggregateCommand : Message.Domain.IAggregateMessage, ICommand
    {
        protected IAggregateCommand(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt) : base(aggregateId, aggregateName, aggregateOccurredAt) { }
        protected IAggregateCommand(string aggregateName) : base(Guid.NewGuid(), aggregateName, DateTime.UtcNow) { }
    }
}