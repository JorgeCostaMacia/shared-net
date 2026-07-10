using global::Serilog.Context;
using global::Serilog.Core.Enrichers;
using JorgeCostaMacia.Quartz.Domain;
using Microsoft.Extensions.Logging;
using Quartz;

namespace JorgeCostaMacia.Quartz.Serilog.Infrastructure;

/// <summary>
/// Quartz job listener that logs every job execution with a <b>fixed, low-cardinality message</b>
/// (<c>JobToBeExecuted</c> / <c>JobExecutionVetoed</c> / <c>JobWasExecuted</c>) and pushes everything
/// variable — scheduler, job, trigger, data, times and the <see cref="JobTrace"/> identifiers —
/// through the Serilog log context, so the events index cheaply and search structurally.
/// </summary>
/// <remarks>
/// <see cref="JobWasExecuted"/> logs at <see cref="LogLevel.Information"/> for a clean execution and
/// at <see cref="LogLevel.Error"/> (with the root cause attached) when the job threw. The trace
/// identifiers come from <see cref="JobTrace.GetOrCreate(IJobExecutionContext)"/> — idempotent, so
/// any other listener of the same execution (e.g. an event-publishing one) reads the same pair
/// regardless of registration order.
/// </remarks>
public sealed class JobsLoggerListener : IJobListener
{
    private readonly ILogger<JobsLoggerListener> _logger;

    /// <summary>Creates the listener over the logger it emits through.</summary>
    /// <param name="logger">The logger.</param>
    public JobsLoggerListener(ILogger<JobsLoggerListener> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public string Name => nameof(JobsLoggerListener);

    /// <summary>Logs <c>JobToBeExecuted</c> at <see cref="LogLevel.Information"/> before the job runs.</summary>
    /// <param name="context">The execution context.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        using (PushProperties(context))
        {
            _logger.LogInformation("JobToBeExecuted");
        }

        return Task.CompletedTask;
    }

    /// <summary>Logs <c>JobExecutionVetoed</c> at <see cref="LogLevel.Warning"/> when a trigger listener vetoed the execution.</summary>
    /// <param name="context">The execution context.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        using (PushProperties(context))
        {
            _logger.LogWarning("JobExecutionVetoed");
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Logs <c>JobWasExecuted</c> — at <see cref="LogLevel.Information"/> for a clean execution, at
    /// <see cref="LogLevel.Error"/> with the root cause attached when the job threw.
    /// </summary>
    /// <param name="context">The execution context.</param>
    /// <param name="jobException">The exception the execution produced, or <see langword="null"/>.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
    {
        using (PushProperties(context))
        {
            if (jobException?.GetBaseException() is null)
            {
                _logger.LogInformation("JobWasExecuted");
            }
            else
            {
                _logger.LogError(jobException?.GetBaseException(), "JobWasExecuted");
            }
        }

        return Task.CompletedTask;
    }

    /// <summary>Pushes the execution's variable data — trace identifiers, scheduler, job, trigger, data and times — into the log context, in a single native push.</summary>
    /// <param name="context">The execution context.</param>
    /// <returns>A disposable that pops the pushed properties.</returns>
    private static IDisposable PushProperties(IJobExecutionContext context)
    {
        JobTrace trace = JobTrace.GetOrCreate(context);

        return LogContext.Push(
            new PropertyEnricher("AggregateId", trace.AggregateId),
            new PropertyEnricher("CorrelationId", trace.CorrelationId),
            new PropertyEnricher("Scheduler", context.Scheduler.SchedulerName),
            new PropertyEnricher("Trigger", context.Trigger.Key.Name),
            new PropertyEnricher("TriggerGroup", context.Trigger.Key.Group),
            new PropertyEnricher("Job", context.JobDetail.Key.Name),
            new PropertyEnricher("JobGroup", context.JobDetail.Key.Group),
            new PropertyEnricher("JobData", context.MergedJobDataMap, destructureObjects: true),
            new PropertyEnricher("ScheduleTime", context.ScheduledFireTimeUtc?.UtcDateTime),
            new PropertyEnricher("FireTime", context.FireTimeUtc.UtcDateTime));
    }
}
