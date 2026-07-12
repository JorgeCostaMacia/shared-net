using JorgeCostaMacia.Quartz.Serilog.IntegrationTests.Support;
using Serilog.Events;

namespace JorgeCostaMacia.Quartz.Serilog.IntegrationTests.Infrastructure;

public class TriggerLoggerListenerIntegrationTests
{
    [Fact]
    public async Task Trigger_FiresAndCompletes_WithTheSchedulerInstructionAndNoNextFire()
    {
        await using SchedulerHarness harness = await SchedulerHarness.StartAsync();

        await harness.RunOnceAsync<DelayJob>(TestContext.Current.CancellationToken);

        IReadOnlyList<LogEvent> events = harness.Sink.Snapshot();
        LogEvent fired = Assert.Single(events, logEvent => logEvent.MessageTemplate.Text == "TriggerFired");
        LogEvent complete = Assert.Single(events, logEvent => logEvent.MessageTemplate.Text == "TriggerComplete");

        Assert.Equal(LogEventLevel.Information, fired.Level);
        Assert.Equal(LogEventLevel.Information, complete.Level);

        // the scheduler's resulting instruction rides on the completion event
        Assert.True(complete.Properties.ContainsKey("TriggerResult"));

        // a one-shot trigger has no next fire once complete
        Assert.True(complete.Properties.ContainsKey("NextFireTime"));
        Assert.Null(((ScalarValue)complete.Properties["NextFireTime"]).Value);
    }
}
