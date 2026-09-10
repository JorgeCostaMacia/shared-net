using Quartz;

namespace JorgeCostaMacia.Quartz.Serilog.Tests.Fakes;

/// <summary>
/// Execution-context double over a real (never started) in-memory scheduler and real job/trigger
/// keys — everything the listeners read, plus the per-firing data map the trace travels in.
/// </summary>
internal sealed class JobExecutionContextFake : IJobExecutionContext
{

    public JobExecutionContextFake(IScheduler scheduler, IJobDetail jobDetail, ITrigger trigger)
    {
        Scheduler = scheduler;
        JobDetail = jobDetail;
        Trigger = trigger;
    }

    /// <summary>Builds a fake over a fresh in-memory scheduler and an <c>orders</c> job/trigger pair.</summary>
    public static async Task<JobExecutionContextFake> Create()
    {
        IScheduler scheduler = await QuartzSchedulerBuilder
            .Create(quartz => quartz
                .ConfigureScheduler(options => options.InstanceName = $"test-{Guid.NewGuid():N}")
                .UseDefaultThreadPool(1)
                .UseInMemoryStore())
            .BuildScheduler();

        IJobDetail job = JobBuilder.Create<NoOpJob>().WithIdentity("job-1", "orders").Build();
        ITrigger trigger = TriggerBuilder.Create().ForJob(job).WithIdentity("trigger-1", "orders").StartNow().Build();

        return new JobExecutionContextFake(scheduler, job, trigger);
    }

    /// <summary>
    /// The same firing with its timestamps the other way round: no scheduled time, a next fire due.
    /// Both listeners guard the two with <c>?.</c>, so this is the side <see cref="Create"/> does not reach.
    /// </summary>
    public JobExecutionContextFake WithFlippedTimestamps()
        => new JobExecutionContextFake(Scheduler, JobDetail, Trigger)
        {
            ScheduledFireTimeUtc = null,
            NextFireTimeUtc = DateTimeOffset.UtcNow.AddMinutes(5)
        };

    public IScheduler Scheduler { get; }
    public ITrigger Trigger { get; }
    public ICalendar? Calendar => null;
    public bool Recovering => false;
    public TriggerKey RecoveringTriggerKey => throw new NotImplementedException();
    public int RefireCount => 0;
    public int RetryAttempt => 0;
    public JobDataMap MergedJobDataMap { get; } = new JobDataMap();
    public IJobDetail JobDetail { get; }
    public IJob JobInstance => throw new NotImplementedException();
    public DateTimeOffset FireTimeUtc { get; } = DateTimeOffset.UtcNow;
    // Settable: the listeners guard both timestamps with ?., so a test needs to drive the nullness
    // from either side — Quartz leaves NextFireTimeUtc empty on a one-shot and ScheduledFireTimeUtc
    // empty on a manual trigger.
    public DateTimeOffset? ScheduledFireTimeUtc { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? PreviousFireTimeUtc { get; init; }
    public DateTimeOffset? NextFireTimeUtc { get; init; }
    public string FireInstanceId => "fire-1";
    public object? Result { get; set; }
    public TimeSpan JobRunTime => TimeSpan.Zero;
    public CancellationToken CancellationToken => CancellationToken.None;

    /// <summary>Inert job type for the fake's job detail — never executed.</summary>
    internal sealed class NoOpJob : IJob
    {
        public ValueTask Execute(IJobExecutionContext context, CancellationToken cancellationToken) => ValueTask.CompletedTask;
    }
}
