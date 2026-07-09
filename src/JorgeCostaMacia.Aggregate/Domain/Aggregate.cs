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
    private List<IDomainEvent> DomainEvents { get; init; }

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
        DomainEvents = new List<IDomainEvent>();
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is null.</exception>
    public void AddDomainEvents(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        DomainEvents.Add(domainEvent);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is null.</exception>
    public void AddDomainEvents(IEnumerable<IDomainEvent> domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        DomainEvents.AddRange(domainEvent);
    }

    /// <inheritdoc/>
    public IEnumerable<IDomainEvent> PullDomainEvents()
    {
        IEnumerable<IDomainEvent> domainEventsAux = DomainEvents.ToList();
        DomainEvents.Clear();

        return domainEventsAux;
    }
}
