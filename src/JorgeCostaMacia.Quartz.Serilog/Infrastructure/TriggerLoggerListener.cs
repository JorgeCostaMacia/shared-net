using global::Serilog.Context;
using global::Serilog.Core.Enrichers;
using JorgeCostaMacia.Quartz.Domain;
using Microsoft.Extensions.Logging;
using Quartz;

namespace JorgeCostaMacia.Quartz.Serilog.Infrastructure;

/// <summary>
/// Quartz trigger listener that logs every trigger transition with a <b>fixed, low-cardinality
/// message</b> (<c>TriggerFired</c> / <c>VetoJobExecution</c> / <c>TriggerMisfired</c> /
/// <c>TriggerComplete</c>) and pushes everything variable — scheduler, job, trigger, data, times and
/// the <see cref="JobTrace"/> identifiers — through the Serilog log context.
/// </summary>
/// <remarks>
/// <para>
/// A misfire is logged at <see cref="LogLevel.Error"/>: the trigger missed its scheduled time (the
/// scheduler was down or thread-starved), which operators usually want to alert on. Since a misfire
/// carries no execution context, its trace identifiers are freshly minted for the log event.
/// </para>
/// <para>
/// <see cref="VetoJobExecution"/> only observes — it always returns <see langword="false"/> (never
/// vetoes) and logs the consultation.
/// </para>
/// </remarks>
public sealed class TriggerLoggerListener : ITriggerListener
{
    private readonly ILogger<TriggerLoggerListener> _logger;

    /// <summary>Creates the listener over the logger it emits through.</summary>
    /// <param name="logger">The logger.</param>
    public TriggerLoggerListener(ILogger<TriggerLoggerListener> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public string Name => nameof(TriggerLoggerListener);

    /// <summary>Logs <c>TriggerFired</c> at <see cref="LogLevel.Information"/> when the trigger fires.</summary>
    /// <param name="trigger">The fired trigger.</param>
    /// <param name="context">The execution context.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        using (PushProperties(context))
        {
            _logger.LogInformation("TriggerFired");
        }

        return Task.CompletedTask;
    }

    /// <summary>Logs <c>VetoJobExecution</c> at <see cref="LogLevel.Information"/> and never vetoes.</summary>
    /// <param name="trigger">The fired trigger.</param>
    /// <param name="context">The execution context.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="false"/>, always — this listener only observes.</returns>
    public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
    {
        using (PushProperties(context))
        {
            _logger.LogInformation("VetoJobExecution");
        }

        return Task.FromResult(false);
    }

    /// <summary>
    /// Logs <c>TriggerMisfired</c> at <see cref="LogLevel.Error"/> — the trigger missed its scheduled
    /// fire time. There is no execution context here, so the trace identifiers are freshly minted.
    /// </summary>
    /// <param name="trigger">The misfired trigger.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
    {
        JobTrace trace = JobTrace.Create();

        using (LogContext.Push(
            new PropertyEnricher("AggregateId", trace.AggregateId),
            new PropertyEnricher("CorrelationId", trace.CorrelationId),
            new PropertyEnricher("Trigger", trigger.Key.Name),
            new PropertyEnricher("TriggerGroup", trigger.Key.Group),
            new PropertyEnricher("Job", trigger.JobKey.Name),
            new PropertyEnricher("JobGroup", trigger.JobKey.Group),
            new PropertyEnricher("JobData", trigger.JobDataMap, destructureObjects: true)))
        {
            _logger.LogError("TriggerMisfired");
        }

        return Task.CompletedTask;
    }

    /// <summary>Logs <c>TriggerComplete</c> at <see cref="LogLevel.Information"/>, including the scheduler's resulting instruction.</summary>
    /// <param name="trigger">The completed trigger.</param>
    /// <param name="context">The execution context.</param>
    /// <param name="triggerInstructionCode">The instruction the trigger returned to the scheduler.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
    {
        using (PushProperties(context))
        using (LogContext.PushProperty("TriggerResult", triggerInstructionCode))
        {
            _logger.LogInformation("TriggerComplete");
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
