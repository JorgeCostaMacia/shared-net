using Quartz;

namespace JorgeCostaMacia.Quartz.Tests.Fakes;

/// <summary>Execution-context double — a real per-firing <see cref="JobDataMap"/> (all <see cref="JobTrace"/> needs), inert everywhere else.</summary>
internal sealed class JobExecutionContextFake : IJobExecutionContext
{
    public IScheduler Scheduler => throw new NotImplementedException();
    public ITrigger Trigger => throw new NotImplementedException();
    public ICalendar? Calendar => null;
    public bool Recovering => false;
    public TriggerKey RecoveringTriggerKey => throw new NotImplementedException();
    public int RefireCount => 0;
    public int RetryAttempt => 0;
    public JobDataMap MergedJobDataMap { get; } = new JobDataMap();
    public IJobDetail JobDetail => throw new NotImplementedException();
    public IJob JobInstance => throw new NotImplementedException();
    public DateTimeOffset FireTimeUtc => DateTimeOffset.UtcNow;
    public DateTimeOffset? ScheduledFireTimeUtc => null;
    public DateTimeOffset? PreviousFireTimeUtc => null;
    public DateTimeOffset? NextFireTimeUtc => null;
    public string FireInstanceId => "fire-1";
    public object? Result { get; set; }
    public TimeSpan JobRunTime => TimeSpan.Zero;
    public CancellationToken CancellationToken => CancellationToken.None;
}
