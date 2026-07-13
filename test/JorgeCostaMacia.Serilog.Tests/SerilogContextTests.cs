using global::Serilog;
using global::Serilog.Events;
using global::Serilog.Sinks.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// 'Serilog' (the third-party namespace) is referenced with global:: so it isn't shadowed by the
// enclosing 'JorgeCostaMacia.Serilog' package namespace in test code.
namespace JorgeCostaMacia.Serilog.Tests;

public class SerilogContextTests
{
    private static IConfiguration EmptyConfiguration() => new ConfigurationBuilder().Build();

    private static IConfiguration InMemorySinkConfiguration() => new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Serilog:Using:0"] = "Serilog.Sinks.InMemory",
            ["Serilog:MinimumLevel:Default"] = "Debug",
            ["Serilog:WriteTo:0:Name"] = "InMemory",
        })
        .Build();

    [Fact]
    public void AddSerilogContext_ReturnsSameServiceCollection_ForChaining()
    {
        ServiceCollection services = new ServiceCollection();

        Assert.Same(services, services.AddSerilogContext(EmptyConfiguration()));
    }

    [Fact]
    public void AddSerilogContext_RegistersSerilogLogger()
    {
        ServiceProvider provider = new ServiceCollection().AddSerilogContext(EmptyConfiguration()).BuildServiceProvider();

        Assert.NotNull(provider.GetService<ILogger>());
    }

    [Fact]
    public void AddSerilogContext_WithEmptyConfiguration_DoesNotThrow()
    {
        ServiceProvider provider = new ServiceCollection().AddSerilogContext(EmptyConfiguration()).BuildServiceProvider();

        Assert.NotNull(provider.GetService<ILogger>());
    }

    [Fact]
    public void AddSerilogContext_HonorsMinimumLevelFromConfiguration()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { ["Serilog:MinimumLevel:Default"] = "Warning" })
            .Build();
        ILogger logger = new ServiceCollection().AddSerilogContext(configuration).BuildServiceProvider().GetRequiredService<ILogger>();

        Assert.True(logger.IsEnabled(LogEventLevel.Warning));
        Assert.False(logger.IsEnabled(LogEventLevel.Debug));
    }

    [Fact]
    public void AddSerilogContext_EnrichesEveryEntry_WithTheBaselineProperties()
    {
        ILogger logger = new ServiceCollection().AddSerilogContext(InMemorySinkConfiguration()).BuildServiceProvider().GetRequiredService<ILogger>();

        logger.Information("probe");

        LogEvent entry = InMemorySink.Instance.LogEvents.Last();
        Assert.True(entry.Properties.ContainsKey("Version"));
        Assert.True(entry.Properties.ContainsKey("Application"));
        Assert.True(entry.Properties.ContainsKey("ThreadId"));
        Assert.True(entry.Properties.ContainsKey("ProcessId"));
    }

    [Fact]
    public void AddSerilogContext_Application_UsesTheSimpleAssemblyName_NotTheFullName()
    {
        ILogger logger = new ServiceCollection().AddSerilogContext(InMemorySinkConfiguration()).BuildServiceProvider().GetRequiredService<ILogger>();

        logger.Information("probe-name");

        LogEvent entry = InMemorySink.Instance.LogEvents.Last();
        string application = ((ScalarValue)entry.Properties["Application"]).Value?.ToString() ?? "";

        // The full name would contain ", Version=..."; the simple assembly name must not.
        Assert.DoesNotContain(",", application);
    }
}
