namespace Shared.Bus.Command.Domain;

public abstract class IAggregateCommand(Guid aggregateId, string aggregateName, DateTime aggregateOccurredAt) : Message.Domain.IAggregateMessage(aggregateId, aggregateName, aggregateOccurredAt), ICommand
{
    public IAggregateCommand(string aggregateName) : this(Guid.NewGuid(), aggregateName, DateTime.UtcNow) { }
}