﻿using Shared.Bus.Event.Domain;

namespace Shared.Entity.Domain;

public abstract class IAggregateEntity : IEntity
{
    private List<IEvent> AggregateEvents { get; init; }

    protected IAggregateEntity(IEnumerable<IEvent> aggregateEvents)
    {
        AggregateEvents = aggregateEvents.ToList();
    }

    protected IAggregateEntity() : this(new List<IEvent>()) { }

    public void AddAggregateEvents(IEvent aggregateEvent) => AggregateEvents.Add(aggregateEvent);
    public void AddAggregateEvents(IEnumerable<IEvent> aggregateEvent) => AggregateEvents.AddRange(aggregateEvent);

    public IEnumerable<IEvent> PullAggregateEvents()
    {
        IEnumerable<IEvent> AggregateEventsAux = new List<IEvent>(AggregateEvents);
        AggregateEvents.Clear();

        return AggregateEventsAux;
    }
}