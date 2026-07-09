using JorgeCostaMacia.DomainEvent.Domain;

namespace JorgeCostaMacia.Aggregate.Domain;

/// <summary>
/// Defines the contract for an <b>Aggregate Root</b> within the Domain-Driven Design (DDD)
/// and Event Sourcing patterns.
/// </summary>
/// <remarks>
/// <para>
/// The <see cref="IAggregate"/> interface ensures that the Aggregate Root, the boundary
/// of transactional consistency, can track and manage the domain events that occur
/// during its lifecycle.
/// </para>
///
/// <para>
/// This contract is essential for enabling the aggregate to publish its state changes
/// (as <see cref="IDomainEvent"/> messages) to the rest of the application or external systems.
/// </para>
/// </remarks>
public interface IAggregate
{
    /// <summary>
    /// Adds a single domain event to the aggregate's internal list of captured events.
    /// Events are typically added during state mutation methods.
    /// </summary>
    /// <param name="domainEvent">The domain event that occurred.</param>
    void AddDomainEvents(IDomainEvent domainEvent);

    /// <summary>
    /// Adds a collection of domain events to the aggregate's internal list of captured events.
    /// </summary>
    /// <param name="domainEvent">The collection of domain events that occurred.</param>
    void AddDomainEvents(IEnumerable<IDomainEvent> domainEvent);

    /// <summary>
    /// Retrieves all accumulated domain events and then <b>clears the internal list</b>
    /// of captured events.
    /// </summary>
    /// <remarks>
    /// This method is typically called by the persistence mechanism or the command handler
    /// after successfully processing a command, ensuring events are persisted and then
    /// published exactly once.
    /// </remarks>
    /// <returns>A collection of <see cref="IDomainEvent"/> messages that occurred.</returns>
    IEnumerable<IDomainEvent> PullDomainEvents();
}
