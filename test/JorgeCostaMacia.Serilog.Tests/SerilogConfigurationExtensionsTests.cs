using System.Reflection;
using JorgeCostaMacia.Serilog.Infrastructure;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Sinks.InMemory;

namespace JorgeCostaMacia.Serilog.Tests;

/// <summary>
/// The family logging baseline. It is a <see cref="LoggerConfiguration"/> extension rather than a
/// one-call bootstrap on purpose: the host keeps writing its own <c>AddSerilog</c>, so the composition
/// stays visible where it happens. The assertions run a real logger into an in-memory sink, so what is
/// checked is the properties that reach a log event — not that the method returned something.
/// </summary>
public class SerilogConfigurationExtensionsTests
{
    private static (ILogger Logger, InMemorySink Sink) Build()
    {
        InMemorySink sink = new InMemorySink();
        ILogger logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WithDefaults()
            .WriteTo.Sink(sink)
            .CreateLogger();

        return (logger, sink);
    }

    [Fact]
    public void WithDefaults_ReturnsTheSameConfiguration_SoItChains()
    {
        LoggerConfiguration configuration = new LoggerConfiguration();

        Assert.Same(configuration, configuration.WithDefaults());
    }

    // The two properties no appsettings entry can produce: both are read off the running entry assembly.
    [Fact]
    public void WithDefaults_StampsTheEntryAssemblyVersionAndName()
    {
        (ILogger logger, InMemorySink sink) = Build();

        logger.Information("Message");

        LogEvent logEvent = Assert.Single(sink.LogEvents);
        AssemblyName entryAssembly = Assembly.GetEntryAssembly()!.GetName();
        Assert.Equal(entryAssembly.Version!.ToString(3), Scalar(logEvent, "Version"));
        Assert.Equal(entryAssembly.Name, Scalar(logEvent, "Application"));
    }

    // The enrichers the baseline owns, so they stop being copied into every app's appsettings pair.
    [Fact]
    public void WithDefaults_AttachesTheThreadAndProcessEnrichers()
    {
        (ILogger logger, InMemorySink sink) = Build();

        logger.Information("Message");

        LogEvent logEvent = Assert.Single(sink.LogEvents);
        Assert.True(logEvent.Properties.ContainsKey("ThreadId"));
        Assert.True(logEvent.Properties.ContainsKey("ProcessId"));
    }

    // WithExceptionDetails is the one worth having: it serializes the exception's own data rather than
    // just its message, so the log carries what the catch site saw.
    [Fact]
    public void WithDefaults_AttachesTheExceptionDetails()
    {
        (ILogger logger, InMemorySink sink) = Build();

        logger.Error(new InvalidOperationException("boom"), "Failed");

        LogEvent logEvent = Assert.Single(sink.LogEvents);
        Assert.True(logEvent.Properties.ContainsKey("ExceptionDetail"));
        Assert.Contains("boom", logEvent.Properties["ExceptionDetail"].ToString());
    }

    // FromLogContext is what makes the family's correlation style work: the identifiers are pushed once
    // per scope and every statement inside carries them without naming them.
    [Fact]
    public void WithDefaults_CarriesWhatTheLogContextPushed()
    {
        (ILogger logger, InMemorySink sink) = Build();
        Guid correlationId = Guid.NewGuid();

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            logger.Information("Message");
        }

        Assert.Equal(correlationId.ToString(), Scalar(Assert.Single(sink.LogEvents), "CorrelationId"));
    }

    private static string? Scalar(LogEvent logEvent, string property)
        => ((ScalarValue)logEvent.Properties[property]).Value?.ToString();
}
