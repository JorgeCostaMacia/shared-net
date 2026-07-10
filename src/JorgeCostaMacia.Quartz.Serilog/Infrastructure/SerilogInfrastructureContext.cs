using Microsoft.Extensions.DependencyInjection;

namespace JorgeCostaMacia.Quartz.Serilog.Infrastructure;

/// <summary>
/// Infrastructure-layer service registrations for the package. Composed by
/// <see cref="SerilogContext"/>; not part of the public API.
/// </summary>
internal static class SerilogInfrastructureContext
{
    /// <summary>
    /// Registers the <see cref="JobsLoggerListener"/> and <see cref="TriggerLoggerListener"/> as
    /// singletons, ready to be attached to Quartz (<c>AddJobListener</c> / <c>AddTriggerListener</c>).
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
    internal static IServiceCollection AddSerilogInfrastructureContext(this IServiceCollection services)
    {
        services
            .AddSingleton<JobsLoggerListener>()
            .AddSingleton<TriggerLoggerListener>();

        return services;
    }
}
