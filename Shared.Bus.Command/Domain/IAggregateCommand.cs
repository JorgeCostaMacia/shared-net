namespace Shared.Bus.Command.Domain;

public abstract class IAggregateCommand : Message.Domain.IAggregateMessage, ICommand
{
    protected IAggregateCommand(Guid aggregateId, string aggregateConsumer, Guid aggregateSubscriber, DateTime aggregateOccurredAt) : base(aggregateId, aggregateConsumer, aggregateSubscriber, aggregateOccurredAt) { }
    protected IAggregateCommand(string aggregateConsumer, Guid aggregateSubscriber) : base(aggregateConsumer, aggregateSubscriber) { }
    protected IAggregateCommand(string aggregateConsumer) : base(aggregateConsumer) { }
}