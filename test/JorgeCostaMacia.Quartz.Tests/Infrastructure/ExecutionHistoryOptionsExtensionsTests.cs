using JorgeCostaMacia.Quartz.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace JorgeCostaMacia.Quartz.Tests.Infrastructure;

/// <summary>
/// The family's history bounds, applied to the options Quartz hands the host through its own
/// <c>AddQuartzExecutionHistory</c>.
/// </summary>
public class ExecutionHistoryOptionsExtensionsTests
{
    // Fifteen days, with a count loose enough that the age is what binds on the busiest host.
    [Fact]
    public void WithDefaults_KeepsFifteenDaysAndFiftyThousandEntries()
    {
        ExecutionHistoryOptions options = new ExecutionHistoryOptions().WithDefaults();

        Assert.Equal(TimeSpan.FromDays(15), options.Retention);
        Assert.Equal(50_000, options.MaxEntriesPerScheduler);
    }

    [Fact]
    public void WithDefaults_ReturnsTheSameOptions_SoItChains()
    {
        ExecutionHistoryOptions options = new ExecutionHistoryOptions();

        Assert.Same(options, options.WithDefaults());
    }

    // The way a host uses it: the bounds reach the options the store reads.
    [Fact]
    public void WithDefaults_ThroughAddQuartzExecutionHistory_ReachesTheStore()
    {
        ServiceCollection services = new ServiceCollection();
        services
            .AddQuartz(quartz => quartz.WithPostgresDefaults("retry", "JobsBus", "bus"))
            .AddQuartzExecutionHistory(options => options.WithDefaults());

        ExecutionHistoryOptions options = services.BuildServiceProvider()
            .GetRequiredService<IOptions<ExecutionHistoryOptions>>().Value;

        Assert.Equal(TimeSpan.FromDays(15), options.Retention);
        Assert.Equal(50_000, options.MaxEntriesPerScheduler);
    }
}
