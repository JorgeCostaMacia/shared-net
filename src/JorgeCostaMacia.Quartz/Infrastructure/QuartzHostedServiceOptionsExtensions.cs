using Quartz;

namespace JorgeCostaMacia.Quartz.Infrastructure;

/// <summary>
/// Extensions for <see cref="QuartzHostedServiceOptions"/> that apply the family's hosted scheduler policy.
/// </summary>
/// <remarks>
/// An extension on the options Quartz hands the host, not an <c>Add…</c> facade: the host keeps writing
/// its own <c>AddQuartzHostedService</c>, so what it composes stays visible in its <c>Program</c> — the
/// same shape as <see cref="QuartzBuilderExtensions.WithPostgresDefaults"/> and
/// <see cref="ExecutionHistoryOptionsExtensions.WithDefaults"/>.
/// </remarks>
public static class QuartzHostedServiceOptionsExtensions
{
    /// <summary>
    /// Starts the scheduler once the host has started, and lets running jobs finish when it stops.
    /// </summary>
    /// <param name="options">The hosted service options to configure.</param>
    /// <returns>The same <paramref name="options"/>, for chaining.</returns>
    /// <remarks>
    /// <para>
    /// <c>AwaitApplicationStarted</c>: the first firing waits for every other hosted service to be up —
    /// the bus among them — so a job never runs against a host that is still composing itself.
    /// </para>
    /// <para>
    /// <c>WaitForJobsToComplete</c>: a stop, a redeploy or a scale-down lets the running firings end
    /// instead of cutting them off half way. With the clustered store a cut-off firing would be recovered
    /// by another node anyway, but only for jobs that request recovery; this is what keeps the others whole.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// builder.Services
    ///     .AddQuartz(quartz => quartz.WithPostgresDefaults("pasarela", "Jobs", "scheduler"))
    ///     .AddQuartzHostedService(options => options.WithDefaults());
    /// </code>
    /// </example>
    public static QuartzHostedServiceOptions WithDefaults(this QuartzHostedServiceOptions options)
    {
        options.AwaitApplicationStarted = true;
        options.WaitForJobsToComplete = true;

        return options;
    }
}
