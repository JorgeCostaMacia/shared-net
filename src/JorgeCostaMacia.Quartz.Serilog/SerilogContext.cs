using JorgeCostaMacia.Quartz.Serilog.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace JorgeCostaMacia.Quartz.Serilog;

/// <summary>
/// Composition root for the <c>JorgeCostaMacia.Quartz.Serilog</c> package: the single public entry
/// point that wires everything the package provides into the application's dependency injection
/// container, composing the package's internal per-layer contexts.
/// </summary>
public static class SerilogContext
{
    /// <summary>
    /// Registers every service exposed by the package — the Quartz job and trigger log listeners.
    /// Attach them in your Quartz configuration afterwards:
    /// <c>q.AddJobListener&lt;JobsLoggerListener&gt;(); q.AddTriggerListener&lt;TriggerLoggerListener&gt;();</c>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
    public static IServiceCollection AddSerilogContext(this IServiceCollection services)
    {
        services
            .AddSerilogInfrastructureContext();

        return services;
    }
}
