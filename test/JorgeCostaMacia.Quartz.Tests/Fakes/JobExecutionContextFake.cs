using Quartz;

namespace JorgeCostaMacia.Quartz.Tests.Fakes;

/// <summary>Execution-context double — a real Get/Put bag (all <see cref="JobTrace"/> needs), inert everywhere else.</summary>
internal sealed class JobExecutionContextFake : IJobExecutionContext
{
    private readonly Dictionary<object, object> _items = new Dictionary<object, object>();

    public void Put(object key, object objectValue) => _items[key] = objectValue;

    public object? Get(object key) => _items.TryGetValue(key, out object? value) ? value : null;

    public IScheduler Scheduler => throw new NotImplementedException();
    public ITrigger Trigger => throw new NotImplementedException();
    public ICalendar? Calendar => null;
    public bool Recovering => false;
    public TriggerKey RecoveringTriggerKey => throw new NotImplementedException();
    public int RefireCount => 0;
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
