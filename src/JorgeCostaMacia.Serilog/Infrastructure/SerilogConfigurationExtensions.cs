using System.Reflection;
using Serilog;
using Serilog.Exceptions;

namespace JorgeCostaMacia.Serilog.Infrastructure;

/// <summary>
/// Extensions for <see cref="LoggerConfiguration"/> that apply the family logging baseline.
/// </summary>
/// <remarks>
/// Kept as a <see cref="LoggerConfiguration"/> extension (not a hidden <c>Add…</c> facade) so the host's
/// own <c>AddSerilog</c> call stays visible in its <c>Program</c> while the baseline lives here — the same
/// shape the <c>Http</c> options extensions use.
/// <para>
/// The baseline owns the enrichment, so <b>do not repeat it in configuration</b>: an app's <c>Serilog</c>
/// section needs only <c>Using</c> for its sinks, <c>MinimumLevel</c>, <c>Override</c> and <c>WriteTo</c>.
/// The <c>Enrich</c> array and the enricher assemblies in <c>Using</c> come from here instead, which is
/// what stops the same four entries being copied into every app's appsettings pair.
/// </para>
/// </remarks>
public static class SerilogConfigurationExtensions
{
    /// <summary>
    /// Applies the baseline: <c>FromLogContext</c>, so the correlation identifiers pushed into a scope
    /// reach every statement inside it; the thread, process and exception-detail enrichers; and the
    /// <c>Version</c> and <c>Application</c> properties taken from the entry assembly — the host or
    /// worker that starts the process, which no configuration file can read.
    /// </summary>
    /// <param name="configuration">The logger configuration to enrich.</param>
    /// <returns>The same <paramref name="configuration"/>, for chaining.</returns>
    /// <remarks>
    /// Do not repeat <c>Application</c> in the <c>Serilog:Properties</c> configuration section: Serilog
    /// adds properties <i>if absent</i>, so a value there wins over this one and the assembly name never
    /// reaches the log.
    /// </remarks>
    public static LoggerConfiguration WithDefaults(this LoggerConfiguration configuration)
    {
        configuration
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("Version", Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3) ?? "0.0.0")
            .Enrich.WithProperty("Application", Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty);

        return configuration;
    }
}
