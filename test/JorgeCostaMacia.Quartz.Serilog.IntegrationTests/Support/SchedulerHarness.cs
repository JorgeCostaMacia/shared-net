using System.Collections.Specialized;
using JorgeCostaMacia.Quartz.Serilog.Infrastructure;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace JorgeCostaMacia.Quartz.Serilog.IntegrationTests.Support;

/// <summary>
/// A real, in-process Quartz scheduler (RAM store) with both logger listeners wired to a capturing
/// Serilog sink — so the listeners observe the execution context Quartz actually produces, not a
/// hand-written fake. There is no store to containerise here; a real scheduler is what an integration
/// test of these listeners needs.
/// </summary>
internal sealed class SchedulerHarness : IAsyncDisposable
{
    private readonly IScheduler scheduler;

    private SchedulerHarness(IScheduler scheduler, CapturingSink sink)
    {
        this.scheduler = scheduler;
        Sink = sink;
    }

    /// <summary>The sink capturing everything both listeners emit during the run.</summary>
    public CapturingSink Sink { get; }

    /// <summary>Starts a scheduler with both listeners registered over a fresh capturing sink.</summary>
    public static async Task<SchedulerHarness> StartAsync()
    {
        CapturingSink sink = new();
        ILoggerFactory loggerFactory = new SerilogLoggerFactory(
            new LoggerConfiguration().MinimumLevel.Verbose().Enrich.FromLogContext().WriteTo.Sink(sink).CreateLogger());

        IScheduler scheduler = await new StdSchedulerFactory(new NameValueCollection
        {
            ["quartz.scheduler.instanceName"] = $"itest-{Guid.NewGuid():N}",
            ["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz",
            ["quartz.threadPool.threadCount"] = "1"
        }).GetScheduler();

        scheduler.ListenerManager.AddJobListener(new JobsLoggerListener(loggerFactory.CreateLogger<JobsLoggerListener>()));
        scheduler.ListenerManager.AddTriggerListener(new TriggerLoggerListener(loggerFactory.CreateLogger<TriggerLoggerListener>()));

        await scheduler.Start();

        return new SchedulerHarness(scheduler, sink);
    }

    /// <summary>
    /// Schedules a single one-shot fire of <typeparamref name="TJob"/> and waits until the whole
    /// callback sequence has been logged. <c>TriggerComplete</c> is the last event Quartz raises, so
    /// waiting on it guarantees <c>TriggerFired</c>, <c>JobToBeExecuted</c> and <c>JobWasExecuted</c>
    /// are already captured.
    /// </summary>
    public async Task RunOnceAsync<TJob>(CancellationToken cancellationToken)
        where TJob : IJob
    {
        IJobDetail job = JobBuilder.Create<TJob>().WithIdentity("job-1", "orders").Build();
        ITrigger trigger = TriggerBuilder.Create().ForJob(job).WithIdentity("trigger-1", "orders").StartNow().Build();

        await scheduler.ScheduleJob(job, trigger, cancellationToken);
        await WaitForAsync("TriggerComplete", cancellationToken);
    }

    private async Task WaitForAsync(string message, CancellationToken cancellationToken)
    {
        for (int attempt = 0; attempt < 200; attempt++)
        {
            if (Sink.Snapshot().Any(logEvent => logEvent.MessageTemplate.Text == message))
            {
                return;
            }

            await Task.Delay(50, cancellationToken);
        }

        throw new TimeoutException($"'{message}' was not logged within the timeout.");
    }

    public async ValueTask DisposeAsync() => await scheduler.Shutdown(waitForJobsToComplete: false);
}
