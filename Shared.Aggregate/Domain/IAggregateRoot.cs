using Shared.Bus.Event.Domain;

namespace Shared.Aggregate.Domain;

public abstract class IAggregateRoot : IAggregate
{
    private List<IEvent> AggregateEvents { get; init; }

    protected IAggregateRoot(IEnumerable<IEvent> aggregateEvents)
    {
        AggregateEvents = aggregateEvents.ToList();
    }

    protected IAggregateRoot() : this(new List<IEvent>()) { }

    public void AddAggregateEvents(IEvent aggregateEvent) => AggregateEvents.Add(aggregateEvent);
    public void AddAggregateEvents(IEnumerable<IEvent> aggregateEvent) => AggregateEvents.AddRange(aggregateEvent);

    public IEnumerable<IEvent> PullAggregateEvents()
    {
        IEnumerable<IEvent> AggregateEventsAux = new List<IEvent>(AggregateEvents);
        AggregateEvents.Clear();

        return AggregateEventsAux;
    }
}