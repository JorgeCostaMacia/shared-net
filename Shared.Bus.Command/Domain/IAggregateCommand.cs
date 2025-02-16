using System.Collections.Immutable;

namespace Shared.Bus.Command.Domain;

public abstract record IAggregateCommand : Message.Domain.IAggregateMessage, ICommand
{
    public Guid CorrelationId => AggregateId;

    protected IAggregateCommand(Guid aggregateId, DateTime aggregateOccurredAt) : base(aggregateId, aggregateOccurredAt) { }
    protected IAggregateCommand() : base() { }
}