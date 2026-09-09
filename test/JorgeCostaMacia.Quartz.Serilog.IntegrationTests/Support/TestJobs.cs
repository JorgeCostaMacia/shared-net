using Quartz;

namespace JorgeCostaMacia.Quartz.Serilog.IntegrationTests.Support;

/// <summary>A job that does real (brief) work, so the execution has a non-zero run time to observe.</summary>
internal sealed class DelayJob : IJob
{
    public async ValueTask Execute(IJobExecutionContext context, CancellationToken cancellationToken) => await Task.Delay(TimeSpan.FromMilliseconds(30), cancellationToken);
}

/// <summary>A job that throws a plain exception, so the listener's error path sees a real Quartz-wrapped fault.</summary>
internal sealed class ThrowingJob : IJob
{
    public async ValueTask Execute(IJobExecutionContext context, CancellationToken cancellationToken)
    {
        await Task.Yield();

        throw new InvalidOperationException("boom");
    }
}
