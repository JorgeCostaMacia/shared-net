using System.Collections.Immutable;

namespace Shared.Bus.Command.Domain;

public abstract record IAggregateCommand : Message.Domain.IAggregateMessage, ICommand
{
    protected IAggregateCommand(Guid aggregateId, DateTime aggregateOccurredAt, ImmutableList<string> aggregateConsumers) : base(aggregateId, aggregateOccurredAt, aggregateConsumers) { }
    protected IAggregateCommand(List<string> aggregateConsumers) : base(aggregateConsumers) { }
    protected IAggregateCommand() : base() { }
}