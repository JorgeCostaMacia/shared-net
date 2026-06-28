namespace JorgeCostaMacia.Entity.Domain;

/// <summary>
/// Defines the foundational contract for any <b>Domain Entity</b> within the application.
/// Entities are objects fundamentally defined by their identity, continuity, and lifecycle,
/// rather than by their attributes.
/// </summary>
/// <remarks>
/// <para>
/// In Domain-Driven Design (DDD), an Entity has the following key characteristics:
/// </para>
/// <list type="bullet">
///     <item><description><b>Identity:</b> Has a unique identifier (ID) that persists across changes in state. Two entities are considered equal if their IDs are equal, regardless of their attributes.</description></item>
///     <item><description><b>Mutability:</b> Its attributes and state can change throughout its lifecycle, unlike Value Objects.</description></item>
///     <item><description><b>Behavior:</b> Encapsulates business logic related to its own state and identity.</description></item>
/// </list>
///
/// <para>
/// <b>Role in Aggregates:</b> entities typically exist within the consistency boundary of an
/// <b>Aggregate Root</b>. If an entity is the root of that boundary, it is itself the Aggregate Root.
/// </para>
///
/// <para>
/// <see cref="IEntity"/> intentionally has no members; it acts as a <b>marker contract</b> that
/// facilitates the identification, loading, and manipulation of domain entities across
/// persistence and application layers.
/// </para>
///
/// <para><b>Usage Examples:</b></para>
/// <list type="bullet">
///     <item><description>A <c>Customer</c> object is an Entity: its properties (name, address) can change, but its ID remains constant.</description></item>
///     <item><description>An <c>OrderLine</c> entity within an <c>Order</c> aggregate is identified by its own ID, distinct from being the Aggregate Root of that boundary.</description></item>
/// </list>
/// </remarks>
public interface IEntity { }
