using Shared.Aggregate.Domain;
using Shared.Bus.Event.Domain;

namespace Shared.Root.Domain;

public abstract class IAggregateRoot(IEnumerable<IEvent> aggregateEvents) : IRoot, IAggregate
{
    private List<IEvent> AggregateEvents { get; init; } = aggregateEvents.ToList();

    public void AddAggregateEvents(IEvent aggregateEvent) => AggregateEvents.Add(aggregateEvent);
    public void AddAggregateEvents(IEnumerable<IEvent> aggregateEvent) => AggregateEvents.AddRange(aggregateEvent);

    public IEnumerable<IEvent> PullAggregateEvents()
    {
        IEnumerable<IEvent> AggregateEventsAux = AggregateEvents;
        AggregateEvents.Clear();

        return AggregateEventsAux;
    }
}