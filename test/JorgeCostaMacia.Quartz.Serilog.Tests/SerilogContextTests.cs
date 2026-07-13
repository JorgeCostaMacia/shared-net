using JorgeCostaMacia.Quartz.Serilog.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace JorgeCostaMacia.Quartz.Serilog.Tests;

public class SerilogContextTests
{
    [Fact]
    public void AddSerilogContext_RegistersBothListenersAsSingletons()
    {
        ServiceCollection services = new ServiceCollection();

        services.AddSerilogContext();

        ServiceDescriptor jobs = Assert.Single(services, e => e.ServiceType == typeof(JobsLoggerListener));
        ServiceDescriptor triggers = Assert.Single(services, e => e.ServiceType == typeof(TriggerLoggerListener));
        Assert.Equal(ServiceLifetime.Singleton, jobs.Lifetime);
        Assert.Equal(ServiceLifetime.Singleton, triggers.Lifetime);
    }
}
