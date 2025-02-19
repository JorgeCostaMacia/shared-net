using System.Collections.Immutable;

namespace Shared.Bus.Command.Domain;

public abstract record IAggregateCommand : Message.Domain.IAggregateMessage, ICommand
{
    protected IAggregateCommand(Guid aggregateId, Guid aggregateCorrelationId, DateTime aggregateOccurredAt, ImmutableList<string> aggregateSubscribers) : base(aggregateId, aggregateCorrelationId, aggregateOccurredAt, aggregateSubscribers) { }
    protected IAggregateCommand(Guid? aggregateId = null, Guid? aggregateCorrelationId = null, DateTime? aggregateOccurredAt = null, IEnumerable<string>? aggregateSubscribers = null) : base(aggregateId, aggregateCorrelationId, aggregateOccurredAt, aggregateSubscribers) { }
}