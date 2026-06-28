namespace JorgeCostaMacia.DomainEvent.Domain;

/// <summary>
/// Marker interface for a <b>Domain Event</b> — an immutable fact describing something that
/// has already happened in the domain. Aggregates raise domain events as a result of state changes.
/// </summary>
/// <remarks>
/// <para>
/// This is the <b>transport-agnostic</b> domain contract: the domain depends only on this marker,
/// with no knowledge of how (or whether) the event travels over a message bus.
/// </para>
///
/// <para>
/// A messaging layer may specialize it — e.g. a bus event interface that also adds message,
/// tracing and routing concerns (<c>IEvent : IDomainEvent, IMessage, ...</c>) — so that concrete
/// events fit an aggregate's event list while the domain stays free of transport concerns.
/// </para>
///
/// <para>
/// <see cref="IDomainEvent"/> intentionally has no members; it is a marker contract.
/// </para>
/// </remarks>
public interface IDomainEvent { }
