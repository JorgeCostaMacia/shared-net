using JorgeCostaMacia.Quartz.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace JorgeCostaMacia.Quartz.Tests.Infrastructure;

/// <summary>
/// The family's hosted scheduler policy, applied to the options Quartz hands the host through its own
/// <c>AddQuartzHostedService</c>.
/// </summary>
public class QuartzHostedServiceOptionsExtensionsTests
{
    [Fact]
    public void WithDefaults_StartsAfterTheHostAndLetsRunningJobsFinish()
    {
        QuartzHostedServiceOptions options = new QuartzHostedServiceOptions().WithDefaults();

        Assert.True(options.AwaitApplicationStarted);
        Assert.True(options.WaitForJobsToComplete);
    }

    [Fact]
    public void WithDefaults_ReturnsTheSameOptions_SoItChains()
    {
        QuartzHostedServiceOptions options = new QuartzHostedServiceOptions();

        Assert.Same(options, options.WithDefaults());
    }

    // The way a host uses it: the policy reaches the options the hosted service reads, which are named
    // per scheduler, so it is read under the scheduler's own name.
    [Fact]
    public void WithDefaults_ThroughAddQuartzHostedService_ReachesTheHostedService()
    {
        ServiceCollection services = new ServiceCollection();
        services
            .AddQuartz(quartz => quartz.WithPostgresDefaults("pasarela", "Jobs", "scheduler"))
            .AddQuartzHostedService(options => options.WithDefaults());

        QuartzHostedServiceOptions options = services.BuildServiceProvider()
            .GetRequiredService<IOptionsMonitor<QuartzHostedServiceOptions>>().Get("pasarela");

        Assert.True(options.AwaitApplicationStarted);
        Assert.True(options.WaitForJobsToComplete);
    }
}
