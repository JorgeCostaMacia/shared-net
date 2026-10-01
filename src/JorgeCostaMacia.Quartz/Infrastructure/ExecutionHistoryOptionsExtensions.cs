using Quartz;

namespace JorgeCostaMacia.Quartz.Infrastructure;

/// <summary>
/// Extensions for <see cref="ExecutionHistoryOptions"/> that apply the family's history bounds.
/// </summary>
/// <remarks>
/// An extension on the options Quartz hands the host, not an <c>Add…</c> facade: the host keeps writing
/// its own <c>AddQuartzExecutionHistory</c>, so what it composes stays visible in its <c>Program</c> —
/// the same shape as <see cref="QuartzBuilderExtensions.WithPostgresDefaults"/>.
/// </remarks>
public static class ExecutionHistoryOptionsExtensions
{
    /// <summary>
    /// Keeps 15 days of execution and misfire history, and at most 50 000 entries per scheduler.
    /// </summary>
    /// <param name="options">The history options to configure.</param>
    /// <returns>The same <paramref name="options"/>, for chaining.</returns>
    /// <remarks>
    /// The count is there so that the age is what binds: the busiest host in the family fires some 3 000
    /// times a day, so 15 days of it fit under the count with room to spare. The store applies both bounds
    /// itself, sweeping on its own timer.
    /// </remarks>
    /// <example>
    /// <code>
    /// builder.Services
    ///     .AddQuartz(quartz => quartz.WithPostgresDefaults("retry", "JobsBus", "bus"))
    ///     .AddQuartzExecutionHistory(options => options.WithDefaults());
    /// </code>
    /// </example>
    public static ExecutionHistoryOptions WithDefaults(this ExecutionHistoryOptions options)
    {
        options.Retention = TimeSpan.FromDays(15);
        options.MaxEntriesPerScheduler = 50_000;

        return options;
    }
}
