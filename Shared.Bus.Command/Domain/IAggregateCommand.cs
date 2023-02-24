namespace Shared.Bus.Command.Domain
{
    public class IAggregateCommand : Message.Domain.IAggregateMessage, ICommand
    {
        public IAggregateCommand(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt) { }
        public IAggregateCommand() : base() { }
    }
}