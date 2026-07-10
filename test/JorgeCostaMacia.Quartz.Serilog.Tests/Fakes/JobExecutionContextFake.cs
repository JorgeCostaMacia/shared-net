using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;

namespace JorgeCostaMacia.Quartz.Serilog.Tests.Fakes;

/// <summary>
/// Execution-context double over a real (never started) in-memory scheduler and real job/trigger
/// keys — everything the listeners read, plus the Get/Put bag the trace travels in.
/// </summary>
internal sealed class JobExecutionContextFake : IJobExecutionContext
{
    private readonly Dictionary<object, object> items = [];

    public JobExecutionContextFake(IScheduler scheduler, IJobDetail jobDetail, ITrigger trigger)
    {
        Scheduler = scheduler;
        JobDetail = jobDetail;
        Trigger = trigger;
    }

    /// <summary>Builds a fake over a fresh in-memory scheduler and an <c>orders</c> job/trigger pair.</summary>
    public static async Task<JobExecutionContextFake> Create()
    {
        IScheduler scheduler = await new StdSchedulerFactory(new NameValueCollection
        {
            ["quartz.scheduler.instanceName"] = $"test-{Guid.NewGuid():N}",
            ["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz",
            ["quartz.threadPool.threadCount"] = "1"
        }).GetScheduler();

        IJobDetail job = JobBuilder.Create<NoOpJob>().WithIdentity("job-1", "orders").Build();
        ITrigger trigger = TriggerBuilder.Create().ForJob(job).WithIdentity("trigger-1", "orders").StartNow().Build();

        return new JobExecutionContextFake(scheduler, job, trigger);
    }

    public void Put(object key, object objectValue) => items[key] = objectValue;

    public object? Get(object key) => items.TryGetValue(key, out object? value) ? value : null;

    public IScheduler Scheduler { get; }
    public ITrigger Trigger { get; }
    public ICalendar? Calendar => null;
    public bool Recovering => false;
    public TriggerKey RecoveringTriggerKey => throw new NotImplementedException();
    public int RefireCount => 0;
    public JobDataMap MergedJobDataMap { get; } = [];
    public IJobDetail JobDetail { get; }
    public IJob JobInstance => throw new NotImplementedException();
    public DateTimeOffset FireTimeUtc { get; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ScheduledFireTimeUtc { get; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? PreviousFireTimeUtc => null;
    public DateTimeOffset? NextFireTimeUtc => null;
    public string FireInstanceId => "fire-1";
    public object? Result { get; set; }
    public TimeSpan JobRunTime => TimeSpan.Zero;
    public CancellationToken CancellationToken => CancellationToken.None;

    /// <summary>Inert job type for the fake's job detail — never executed.</summary>
    internal sealed class NoOpJob : IJob
    {
        public Task Execute(IJobExecutionContext context) => Task.CompletedTask;
    }
}
