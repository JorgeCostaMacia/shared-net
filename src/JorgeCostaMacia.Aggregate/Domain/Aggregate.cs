using JorgeCostaMacia.DomainEvent.Domain;

namespace JorgeCostaMacia.Aggregate.Domain;

/// <summary>
/// Provides the base implementation for managing and tracking <b>Domain Events</b> within
/// an <b>Aggregate Root</b>, implementing the <see cref="IAggregate"/> contract.
/// </summary>
/// <remarks>
/// <para>
/// This class handles the internal storage of events and provides the necessary
/// logic to add events and to pull and clear them, ensuring that events are only
/// published once when the aggregate's state is persisted.
/// </para>
/// </remarks>
public abstract class Aggregate : IAggregate
{
    private List<IDomainEvent> AggregateEvents { get; init; }

    /// <summary>
    /// Initializes a new Aggregate instance with an empty pending-events list.
    /// </summary>
    /// <remarks>
    /// Used both when loading an existing aggregate from persistence — where state is
    /// restored independently of this base class, e.g., by the ORM or a derived class's
    /// own constructor — and when creating a brand-new aggregate from a service payload.
    /// In both cases, there are no domain events pending publication yet; they only
    /// accumulate as a result of operations performed during the current unit of work.
    /// </remarks>
    protected Aggregate()
    {
        AggregateEvents = new List<IDomainEvent>();
    }

    /// <inheritdoc/>
    public void AddAggregateEvents(IDomainEvent aggregateEvent) => AggregateEvents.Add(aggregateEvent);

    /// <inheritdoc/>
    public void AddAggregateEvents(IEnumerable<IDomainEvent> aggregateEvent) => AggregateEvents.AddRange(aggregateEvent);

    /// <inheritdoc/>
    public IEnumerable<IDomainEvent> PullAggregateEvents()
    {
        IEnumerable<IDomainEvent> AggregateEventsAux = AggregateEvents.ToList();
        AggregateEvents.Clear();

        return AggregateEventsAux;
    }
}
