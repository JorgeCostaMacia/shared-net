using JorgeCostaMacia.Quartz.Infrastructure;
using JorgeCostaMacia.Quartz.IntegrationTests.Support;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Quartz;

namespace JorgeCostaMacia.Quartz.IntegrationTests.Infrastructure;

/// <summary>
/// The store configuration against a real Postgres. Quartz 4 validates the schema when the scheduler
/// starts, so nothing below can be asserted from a fake: the options are only built once the store has
/// connected, and a prefix pointing at tables that do not exist fails the start outright.
/// </summary>
[Collection(nameof(PostgreSqlCollection))]
public class QuartzBuilderExtensionsIntegrationTests
{
    private readonly PostgreSqlFixture _postgres;

    public QuartzBuilderExtensionsIntegrationTests(PostgreSqlFixture postgres)
    {
        _postgres = postgres;
    }

    private ServiceProvider Provider(string instance)
    {
        ServiceCollection services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { ["ConnectionStrings:JobsBus"] = _postgres.ConnectionString })
            .Build());

        // ProvisionSchema creates the tables under the prefix; the schema holding them is the fixture's.
        services.AddQuartz(quartz => quartz
            .WithPostgresDefaults(instance, "JobsBus", PostgreSqlFixture.Schema)
            .UsePersistentStore(store => store.ProvisionSchema()));

        return services.BuildServiceProvider();
    }

    // The dot appended to the schema is what makes the store read bus.TRIGGERS rather than the
    // QRTZ_TRIGGERS of Quartz's own default, and the options are only materialized once it connects.
    [Fact]
    public async Task WithPostgresDefaults_AppendsTheDotToTheSchemaAndStoresJobDataAsStrings()
    {
        ServiceProvider provider = Provider("retry-prefix");

        IScheduler scheduler = await provider.GetRequiredService<ISchedulerFactory>()
            .GetScheduler(TestContext.Current.CancellationToken);

        AdoJobStoreOptions options = provider.GetRequiredService<IOptions<AdoJobStoreOptions>>().Value;
        Assert.Equal($"{PostgreSqlFixture.Schema}.", options.TablePrefix);
        Assert.True(options.StoreJobDataAsStrings);
        Assert.NotNull(scheduler);
    }

    // What the store is for: a scheduled job survives in Postgres and comes back with its data. This is
    // also what StoreJobDataAsStrings changes, so it is the assertion that a deployment relies on.
    [Fact]
    public async Task WithPostgresDefaults_PersistsAJobAndItsDataAcrossSchedulers()
    {
        IScheduler first = await Provider("retry-persist").GetRequiredService<ISchedulerFactory>()
            .GetScheduler(TestContext.Current.CancellationToken);

        IJobDetail job = JobBuilder.Create<NoOpJob>()
            .WithIdentity("message-1:0", "orders")
            .UsingJobData("Topic", "orders.v1")
            .StoreDurably()
            .Build();
        ITrigger trigger = TriggerBuilder.Create()
            .ForJob(job)
            .WithIdentity("message-1:0", "orders")
            .StartAt(DateTimeOffset.UtcNow.AddHours(1))
            .Build();

        await first.ScheduleJob(job, trigger, cancellationToken: TestContext.Current.CancellationToken);

        // A second scheduler over the same store reads what the first wrote — the point of persistence.
        IScheduler second = await Provider("retry-persist").GetRequiredService<ISchedulerFactory>()
            .GetScheduler(TestContext.Current.CancellationToken);

        IJobDetail? stored = await second.GetJobDetail(new JobKey("message-1:0", "orders"), TestContext.Current.CancellationToken);

        Assert.NotNull(stored);
        Assert.Equal("orders.v1", stored.JobDataMap.GetString("Topic"));
    }

    /// <summary>A durable job that never runs: these tests assert persistence, not execution.</summary>
    internal sealed class NoOpJob : IJob
    {
        public ValueTask Execute(IJobExecutionContext context, CancellationToken cancellationToken) => ValueTask.CompletedTask;
    }
}
