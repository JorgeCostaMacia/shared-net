using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;

namespace JorgeCostaMacia.Serilog;

/// <summary>
/// Configures centralized Serilog logging: reads the <c>Serilog</c> configuration section and
/// applies a common baseline of enrichers in code, so each app only declares its sinks and levels.
/// </summary>
public static class SerilogContext
{
    /// <summary>
    /// Registers Serilog as the logging provider, reading sinks and minimum levels from the
    /// <c>Serilog</c> configuration section and always enriching every log entry in code with
    /// <c>FromLogContext</c>, <c>ThreadId</c>, <c>ProcessId</c>, exception details, and
    /// <c>Version</c> / <c>Application</c> properties taken from the entry assembly (the host or
    /// worker that starts the process).
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing the <c>Serilog</c> section.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance, to allow method chaining.</returns>
    /// <remarks>
    /// <para>
    /// The enrichers listed above and the <c>Version</c> / <c>Application</c> properties are applied
    /// here in code, so do NOT repeat them in configuration. In particular, because Serilog adds
    /// properties <i>if absent</i>, a <c>Properties:Application</c> entry in config would win over the
    /// code value — leave it out. Your <c>Serilog</c> section only needs <c>Using</c>, <c>WriteTo</c>
    /// and <c>MinimumLevel</c>; anything you add there still composes on top of the code baseline.
    /// </para>
    /// <para>Production <b>appsettings.json</b>:</para>
    /// <code>
    /// {
    ///   "Serilog": {
    ///     "Using": [ "Serilog.Sinks.Console" ],
    ///     "MinimumLevel": {
    ///       "Default": "Error",
    ///       "Override": {
    ///         "Microsoft.Hosting.Lifetime": "Information",
    ///         "YOUR_APP_NAMESPACE": "Information"
    ///       }
    ///     },
    ///     "WriteTo": [
    ///       {
    ///         "Name": "Console",
    ///         "Args": { "formatter": "Serilog.Formatting.Json.JsonFormatter, Serilog" }
    ///       }
    ///     ]
    ///   }
    /// }
    /// </code>
    /// <para>
    /// <b>Override keys:</b> always include the app's own root namespace (here as
    /// <c>YOUR_APP_NAMESPACE</c>) for verbose application-level logging. For a worker or
    /// background service, <c>Microsoft.Hosting.Lifetime</c> is usually enough. For an HTTP API,
    /// also add overrides for <c>Microsoft.AspNetCore.Authentication</c>,
    /// <c>Microsoft.AspNetCore.Authorization</c>,
    /// <c>Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware</c>, and
    /// <c>Serilog.AspNetCore.RequestLoggingMiddleware</c>.
    /// </para>
    /// <para>Development <b>appsettings.Development.json</b>:</para>
    /// <code>
    /// {
    ///   "Serilog": {
    ///     "Using": [ "Serilog.Sinks.Console" ],
    ///     "MinimumLevel": {
    ///       "Default": "Information",
    ///       "Override": {
    ///         "Microsoft.Hosting.Lifetime": "Information",
    ///         "YOUR_APP_NAMESPACE": "Information"
    ///       }
    ///     },
    ///     "WriteTo": [
    ///       {
    ///         "Name": "Console",
    ///         "Args": {
    ///           "theme": "Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme::Code, Serilog.Sinks.Console",
    ///           "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"
    ///         }
    ///       }
    ///     ]
    ///   }
    /// }
    /// </code>
    /// <para>
    /// <b>Console formatting:</b> Production uses <c>JsonFormatter</c> for structured,
    /// machine-parseable output suited to log aggregation tooling; Development uses a themed,
    /// human-readable <c>outputTemplate</c> for easier reading directly in the terminal.
    /// </para>
    /// </remarks>
    public static IServiceCollection AddSerilogContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog((_, config) => config
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("Version", Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3) ?? "0.0.0")
            .Enrich.WithProperty("Application", Assembly.GetEntryAssembly()?.GetName().FullName ?? string.Empty));

        return services;
    }
}
