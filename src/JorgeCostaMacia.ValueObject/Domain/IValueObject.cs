namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Marker interface used to identify domain classes that follow the <b>Value Object</b> pattern.
/// </summary>
/// <remarks>
/// <para>
/// A Value Object is an object that measures, quantifies, or describes a thing in the domain
/// and does not possess conceptual identity.
/// </para>
/// <para>
/// Key characteristics enforced by classes implementing this interface usually include:
/// </para>
/// <list type="bullet">
///     <item><description>
///         <b>Immutability:</b> Once created, its encapsulated value(s) cannot change.
///     </description></item>
///     <item><description>
///         <b>Equality based on value:</b> Two Value Objects are considered equal if all their fields are equal.
///     </description></item>
///     <item><description>
///         <b>Self-Validation:</b> They often include logic to ensure the value(s) are valid domain representations.
///     </description></item>
/// </list>
/// </remarks>
public interface IValueObject { }