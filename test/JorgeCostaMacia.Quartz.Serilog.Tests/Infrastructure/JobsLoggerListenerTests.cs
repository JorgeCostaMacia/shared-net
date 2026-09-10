using JorgeCostaMacia.Quartz.Serilog.Infrastructure;
using JorgeCostaMacia.Quartz.Serilog.Tests.Fakes;
using Microsoft.Extensions.Logging;
using Quartz;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace JorgeCostaMacia.Quartz.Serilog.Tests.Infrastructure;

public class JobsLoggerListenerTests
{
    private readonly CapturingSink _sink = new CapturingSink();

    private JobsLoggerListener Listener()
        => new JobsLoggerListener(new SerilogLoggerFactory(new LoggerConfiguration().MinimumLevel.Verbose().Enrich.FromLogContext().WriteTo.Sink(_sink).CreateLogger()).CreateLogger<JobsLoggerListener>());

    [Fact]
    public async Task JobToBeExecuted_LogsInformation_WithFixedMessageAndContext()
    {
        JobExecutionContextFake context = await JobExecutionContextFake.Create();

        await Listener().JobToBeExecuted(context, TestContext.Current.CancellationToken);

        LogEvent logEvent = Assert.Single(_sink.Events);
        Assert.Equal("JobToBeExecuted", logEvent.MessageTemplate.Text);   // fixed, low-cardinality message
        Assert.Equal(LogEventLevel.Information, logEvent.Level);
        Assert.Equal("\"job-1\"", logEvent.Properties["Job"].ToString());
        Assert.Equal("\"orders\"", logEvent.Properties["JobGroup"].ToString());
        Assert.Equal("\"trigger-1\"", logEvent.Properties["Trigger"].ToString());
        Assert.True(logEvent.Properties.ContainsKey("Scheduler"));
        Assert.True(logEvent.Properties.ContainsKey("AggregateId"));
        Assert.True(logEvent.Properties.ContainsKey("CorrelationId"));
        Assert.True(logEvent.Properties.ContainsKey("FireTime"));
        Assert.Equal("\"fire-1\"", logEvent.Properties["FireInstanceId"].ToString());   // the native per-fire correlation id
        Assert.False(logEvent.Properties.ContainsKey("JobRunTime"));                    // run time is not known until the job completes
    }

    [Fact]
    public async Task JobExecutionVetoed_LogsWarning()
    {
        JobExecutionContextFake context = await JobExecutionContextFake.Create();

        await Listener().JobExecutionVetoed(context, TestContext.Current.CancellationToken);

        LogEvent logEvent = Assert.Single(_sink.Events);
        Assert.Equal("JobExecutionVetoed", logEvent.MessageTemplate.Text);
        Assert.Equal(LogEventLevel.Warning, logEvent.Level);
    }

    [Fact]
    public async Task JobWasExecuted_Clean_LogsInformation()
    {
        JobExecutionContextFake context = await JobExecutionContextFake.Create();

        await Listener().JobWasExecuted(context, jobException: null, TestContext.Current.CancellationToken);

        LogEvent logEvent = Assert.Single(_sink.Events);
        Assert.Equal("JobWasExecuted", logEvent.MessageTemplate.Text);
        Assert.Equal(LogEventLevel.Information, logEvent.Level);
        Assert.Null(logEvent.Exception);
        Assert.True(logEvent.Properties.ContainsKey("JobRunTime"));                     // run time is pushed for the completed event
    }

    [Fact]
    public async Task JobWasExecuted_WithException_LogsError_WithTheRootCause()
    {
        JobExecutionContextFake context = await JobExecutionContextFake.Create();
        InvalidOperationException cause = new InvalidOperationException("boom");

        await Listener().JobWasExecuted(context, new JobExecutionException(cause), TestContext.Current.CancellationToken);

        LogEvent logEvent = Assert.Single(_sink.Events);
        Assert.Equal("JobWasExecuted", logEvent.MessageTemplate.Text);   // same fixed message; the level carries the outcome
        Assert.Equal(LogEventLevel.Error, logEvent.Level);
        Assert.Same(cause, logEvent.Exception);                          // the root cause, not the Quartz wrapper
    }

    [Fact]
    public async Task Callbacks_ShareTheTraceAcrossTheExecution()
    {
        // whoever observes the same execution reads the same identifiers — no ordering contract.
        JobExecutionContextFake context = await JobExecutionContextFake.Create();
        JobsLoggerListener listener = Listener();

        await listener.JobToBeExecuted(context, TestContext.Current.CancellationToken);
        await listener.JobWasExecuted(context, jobException: null, TestContext.Current.CancellationToken);

        Assert.Equal(2, _sink.Events.Count);
        Assert.Equal(_sink.Events[0].Properties["AggregateId"].ToString(), _sink.Events[1].Properties["AggregateId"].ToString());
        Assert.Equal(_sink.Events[0].Properties["CorrelationId"].ToString(), _sink.Events[1].Properties["CorrelationId"].ToString());
    }
    // The other side of the two ?. guards: Quartz leaves ScheduledFireTimeUtc empty on a manual
    // trigger and NextFireTimeUtc empty on a one-shot, so both have to log as null rather than throw.
    // The error path: a real Quartz-wrapped fault has to reach the log with its root cause, which is
    // the other side of the jobException guard.
    [Fact]
    public async Task JobWasExecuted_WithAFault_LogsItAsError()
    {
        JobExecutionContextFake context = await JobExecutionContextFake.Create();
        JobExecutionException fault = new JobExecutionException(new InvalidOperationException("boom"));

        await Listener().JobWasExecuted(context, fault, TestContext.Current.CancellationToken);

        LogEvent logEvent = Assert.Single(_sink.Events);
        Assert.Equal(LogEventLevel.Error, logEvent.Level);
        Assert.NotNull(logEvent.Exception);
        Assert.Equal("boom", logEvent.Exception.Message);
    }

    [Fact]
    public async Task JobToBeExecuted_WithTheTimestampsFlipped_LogsThemAsTheyCome()
    {
        JobExecutionContextFake flipped = (await JobExecutionContextFake.Create()).WithFlippedTimestamps();

        await Listener().JobToBeExecuted(flipped, TestContext.Current.CancellationToken);

        LogEvent logEvent = Assert.Single(_sink.Events);
        Assert.Null(((ScalarValue)logEvent.Properties["ScheduleTime"]).Value);
        Assert.NotNull(((ScalarValue)logEvent.Properties["NextFireTime"]).Value);
    }
}
