using JorgeCostaMacia.Quartz.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace JorgeCostaMacia.Quartz.Tests.Infrastructure;

/// <summary>
/// The clustered Postgres store configuration every host in the family repeats. It is an
/// <see cref="IQuartzBuilder"/> extension rather than a one-call bootstrap, so the host keeps writing its
/// own <c>AddQuartz</c> and the composition stays visible where it happens.
/// </summary>
public class QuartzBuilderExtensionsTests
{
    private static ServiceProvider Provider(string instance = "retry", string connection = "JobsBus", string schema = "bus")
    {
        ServiceCollection services = new ServiceCollection();
        services.AddQuartz(quartz => quartz.WithPostgresDefaults(instance, connection, schema));

        return services.BuildServiceProvider();
    }

    [Fact]
    public void WithPostgresDefaults_ReturnsTheSameBuilder_SoItChains()
    {
        ServiceCollection services = new ServiceCollection();
        IQuartzBuilder? captured = null;
        IQuartzBuilder? returned = null;

        services.AddQuartz(quartz =>
        {
            captured = quartz;
            returned = quartz.WithPostgresDefaults("retry", "JobsBus", "bus");
        });

        Assert.NotNull(captured);
        Assert.Same(captured, returned);
    }

    // The identity the store rows carry: the instance name keeps two schedulers sharing a database apart,
    // and the instance id has to differ per replica or clustering fires a trigger twice.
    [Fact]
    public void WithPostgresDefaults_NamesTheSchedulerAndIdentifiesTheInstance()
    {
        QuartzSchedulerOptions options = Provider(instance: "retry")
            .GetRequiredService<IOptions<QuartzSchedulerOptions>>().Value;

        Assert.Equal("retry", options.InstanceName);
        Assert.Equal(Environment.MachineName, options.InstanceId);
    }

    [Fact]
    public void WithPostgresDefaults_TakesTheInstanceNameGiven()
    {
        QuartzSchedulerOptions options = Provider(instance: "dwh")
            .GetRequiredService<IOptions<QuartzSchedulerOptions>>().Value;

        Assert.Equal("dwh", options.InstanceName);
    }

    // The connection string is resolved by name, so a name that is not in the host's ConnectionStrings
    // section fails the scheduler's start rather than leaving it pointing nowhere. No Postgres needed:
    // it never gets as far as connecting.
    [Fact]
    public async Task WithPostgresDefaults_WithAnUnresolvableConnectionName_FailsTheSchedulerStart()
    {
        ISchedulerFactory factory = Provider(connection: "DoesNotExist").GetRequiredService<ISchedulerFactory>();

        SchedulerConfigException exception = await Assert.ThrowsAsync<SchedulerConfigException>(
            async () => await factory.GetScheduler(TestContext.Current.CancellationToken));

        Assert.Contains("DoesNotExist", exception.Message);
    }
}
