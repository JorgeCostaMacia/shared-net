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
/// <para>
/// Whether an aggregate is loaded from persistence — its state restored independently of this
/// base class, e.g. by the ORM or a derived constructor — or created fresh from a service payload,
/// it starts with no pending domain events. They accumulate only as a result of operations
/// performed during the current unit of work, and are drained by <see cref="PullEvents"/> once
/// the aggregate has been persisted.
/// </para>
/// </remarks>
public abstract class Aggregate : IAggregate
{
    private readonly List<IDomainEvent> _events = new List<IDomainEvent>();

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="event"/> is null.</exception>
    public void AddEvent(IDomainEvent @event)
    {
        ArgumentNullException.ThrowIfNull(@event);

        _events.Add(@event);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="events"/> is null.</exception>
    public void AddEvents(IEnumerable<IDomainEvent> events)
    {
        ArgumentNullException.ThrowIfNull(events);

        _events.AddRange(events);
    }

    /// <inheritdoc/>
    public IEnumerable<IDomainEvent> PullEvents()
    {
        IEnumerable<IDomainEvent> events = _events.ToList();
        _events.Clear();

        return events;
    }
}
