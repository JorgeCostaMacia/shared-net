using JorgeCostaMacia.Quartz.Serilog.IntegrationTests.Support;
using Serilog.Events;

namespace JorgeCostaMacia.Quartz.Serilog.IntegrationTests.Infrastructure;

public class JobsLoggerListenerIntegrationTests
{
    [Fact]
    public async Task SuccessfulJob_LogsBothCallbacks_WithTheContextQuartzActuallyPopulates()
    {
        await using SchedulerHarness harness = await SchedulerHarness.StartAsync();

        await harness.RunOnceAsync<DelayJob>(TestContext.Current.CancellationToken);

        IReadOnlyList<LogEvent> events = harness.Sink.Snapshot();
        LogEvent toBe = Assert.Single(events, logEvent => logEvent.MessageTemplate.Text == "JobToBeExecuted");
        LogEvent was = Assert.Single(events, logEvent => logEvent.MessageTemplate.Text == "JobWasExecuted");

        Assert.Equal(LogEventLevel.Information, was.Level);
        Assert.Null(was.Exception);

        // the native fire id is real, non-empty and stable across the two callbacks of one fire
        string? fireInstanceId = ((ScalarValue)toBe.Properties["FireInstanceId"]).Value as string;
        Assert.False(string.IsNullOrEmpty(fireInstanceId));
        Assert.Equal(fireInstanceId, ((ScalarValue)was.Properties["FireInstanceId"]).Value as string);

        // run time is known only once the job completes, and a job that really ran took real time
        Assert.False(toBe.Properties.ContainsKey("JobRunTime"));
        TimeSpan jobRunTime = Assert.IsType<TimeSpan>(((ScalarValue)was.Properties["JobRunTime"]).Value);
        Assert.True(jobRunTime > TimeSpan.Zero);

        // a one-shot trigger has no next fire once complete
        Assert.True(was.Properties.ContainsKey("NextFireTime"));
        Assert.Null(((ScalarValue)was.Properties["NextFireTime"]).Value);

        // not a recovery re-run, and no immediate refires
        Assert.False(Assert.IsType<bool>(((ScalarValue)was.Properties["Recovering"]).Value));
        Assert.Equal(0, Assert.IsType<int>(((ScalarValue)was.Properties["RefireCount"]).Value));

        // the trace identity is shared across the execution's callbacks (real Get/Put on the context)
        Assert.Equal(((ScalarValue)toBe.Properties["AggregateId"]).Value, ((ScalarValue)was.Properties["AggregateId"]).Value);
    }

    [Fact]
    public async Task ThrowingJob_LogsError_WithTheRootCause()
    {
        await using SchedulerHarness harness = await SchedulerHarness.StartAsync();

        await harness.RunOnceAsync<ThrowingJob>(TestContext.Current.CancellationToken);

        LogEvent was = Assert.Single(harness.Sink.Snapshot(), logEvent => logEvent.MessageTemplate.Text == "JobWasExecuted");
        Assert.Equal(LogEventLevel.Error, was.Level);
        InvalidOperationException cause = Assert.IsType<InvalidOperationException>(was.Exception);   // the root cause, not the Quartz wrapper
        Assert.Equal("boom", cause.Message);
    }
}
