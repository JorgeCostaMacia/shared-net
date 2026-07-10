using JorgeCostaMacia.Quartz.Serilog.Infrastructure;
using JorgeCostaMacia.Quartz.Serilog.Tests.Fakes;
using Microsoft.Extensions.Logging;
using Quartz;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

namespace JorgeCostaMacia.Quartz.Serilog.Tests.Infrastructure;

public class TriggerLoggerListenerTests
{
    private readonly CapturingSink sink = new();

    private TriggerLoggerListener Listener()
        => new(new SerilogLoggerFactory(new LoggerConfiguration().MinimumLevel.Verbose().Enrich.FromLogContext().WriteTo.Sink(sink).CreateLogger()).CreateLogger<TriggerLoggerListener>());

    [Fact]
    public async Task TriggerFired_LogsInformation_WithFixedMessageAndContext()
    {
        JobExecutionContextFake context = await JobExecutionContextFake.Create();

        await Listener().TriggerFired(context.Trigger, context, TestContext.Current.CancellationToken);

        LogEvent logEvent = Assert.Single(sink.Events);
        Assert.Equal("TriggerFired", logEvent.MessageTemplate.Text);
        Assert.Equal(LogEventLevel.Information, logEvent.Level);
        Assert.Equal("\"trigger-1\"", logEvent.Properties["Trigger"].ToString());
        Assert.True(logEvent.Properties.ContainsKey("AggregateId"));
    }

    [Fact]
    public async Task VetoJobExecution_NeverVetoes_AndLogsInformation()
    {
        JobExecutionContextFake context = await JobExecutionContextFake.Create();

        bool vetoed = await Listener().VetoJobExecution(context.Trigger, context, TestContext.Current.CancellationToken);

        Assert.False(vetoed);
        LogEvent logEvent = Assert.Single(sink.Events);
        Assert.Equal("VetoJobExecution", logEvent.MessageTemplate.Text);
        Assert.Equal(LogEventLevel.Information, logEvent.Level);
    }

    [Fact]
    public async Task TriggerMisfired_LogsError_WithFreshTrace()
    {
        JobExecutionContextFake context = await JobExecutionContextFake.Create();

        await Listener().TriggerMisfired(context.Trigger, TestContext.Current.CancellationToken);

        LogEvent logEvent = Assert.Single(sink.Events);
        Assert.Equal("TriggerMisfired", logEvent.MessageTemplate.Text);
        Assert.Equal(LogEventLevel.Error, logEvent.Level);
        Assert.Equal("\"trigger-1\"", logEvent.Properties["Trigger"].ToString());
        Assert.Equal("\"job-1\"", logEvent.Properties["Job"].ToString());
        Assert.True(logEvent.Properties.ContainsKey("AggregateId"));   // freshly minted — a misfire has no execution context
    }

    [Fact]
    public async Task TriggerComplete_LogsInformation_WithTheSchedulerInstruction()
    {
        JobExecutionContextFake context = await JobExecutionContextFake.Create();

        await Listener().TriggerComplete(context.Trigger, context, SchedulerInstruction.NoInstruction, TestContext.Current.CancellationToken);

        LogEvent logEvent = Assert.Single(sink.Events);
        Assert.Equal("TriggerComplete", logEvent.MessageTemplate.Text);
        Assert.Equal(LogEventLevel.Information, logEvent.Level);
        Assert.True(logEvent.Properties.ContainsKey("TriggerResult"));
    }
}
