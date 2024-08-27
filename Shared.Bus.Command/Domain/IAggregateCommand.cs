namespace Shared.Bus.Command.Domain;

public abstract class IAggregateCommand : Message.Domain.IAggregateMessage, ICommand
{
    protected IAggregateCommand(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt) { }
    protected IAggregateCommand() : base() { }
}