namespace Shared.Bus.Command.Domain;

public abstract class IAggregateCommand : Message.Domain.IAggregateMessage, ICommand
{
    protected IAggregateCommand(Guid aggregateId, string aggregateConsumer, DateTime aggregateOccurredAt) : base(aggregateId, aggregateConsumer, aggregateOccurredAt) { }
    protected IAggregateCommand(string aggregateConsumer) : base(aggregateConsumer) { }
}