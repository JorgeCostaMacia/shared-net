namespace JorgeCostaMacia.Service.Domain;

/// <summary>
/// Marker interface that designates a class as the strongly-typed configuration
/// parameters (<c>TOptions</c>) used when declaring a service, rather than the
/// service itself. No members are defined intentionally.
/// </summary>
/// <remarks>
/// <para>
/// Classes implementing this interface represent configuration data bound from settings
/// and used to declare or configure a service — for example, connection strings, timeouts,
/// or other parameters required at registration time.
/// </para>
///
/// <para>
/// These classes are typically consumed elsewhere through one of the standard .NET
/// options abstractions:
/// </para>
/// <list type="bullet">
///   <item><description><see cref="Microsoft.Extensions.Options.IOptions{TOptions}"/> – for immutable configuration snapshots.</description></item>
///   <item><description><see cref="Microsoft.Extensions.Options.IOptionsMonitor{TOptions}"/> – for change-tracking and reloadable configurations.</description></item>
///   <item><description><see cref="Microsoft.Extensions.Options.IOptionsSnapshot{TOptions}"/> – for per-request configurations in scoped lifetimes.</description></item>
/// </list>
///
/// <para>
/// This marker serves purely as a semantic indicator and contains no members. It allows
/// configuration classes to be identified, discovered, and automatically registered
/// (e.g., via assembly scanning and binding to configuration sections) when declaring
/// the services that depend on them.
/// </para>
/// </remarks>
public interface IOptionsService { }
