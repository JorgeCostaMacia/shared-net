namespace JorgeCostaMacia.Service.Domain;

/// <summary>
/// Marker interface that designates a class or interface as a service within the system.
/// </summary>
/// <remarks>
/// <para>
/// This interface is used to identify components that represent domain, application, or infrastructure services.
/// It has no members and serves purely as a semantic indicator.
/// </para>
///
/// <para>
/// Classes implementing <see cref="IService"/> are typically:
/// <list type="bullet">
///   <item><description>Registered in the dependency injection container.</description></item>
///   <item><description>Stateless or transient in nature.</description></item>
///   <item><description>Responsible for performing operations, coordination, or side effects.</description></item>
/// </list>
/// </para>
///
/// <para>
/// Examples of service types include:
/// <list type="bullet">
///   <item><description>Domain services (e.g., <c>IOrderService</c>).</description></item>
///   <item><description>Infrastructure services (e.g., <c>IRepository&lt;T&gt;</c>, <c>IMessageBus</c>).</description></item>
///   <item><description>Application services or handlers.</description></item>
/// </list>
/// </para>
///
/// <para>
/// Entities, value objects, and DTOs should <b>not</b> implement this interface.
/// </para>
/// </remarks>
public interface IService { }
