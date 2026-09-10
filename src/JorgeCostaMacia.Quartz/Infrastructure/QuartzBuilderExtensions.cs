using Quartz;

namespace JorgeCostaMacia.Quartz.Infrastructure;

/// <summary>
/// Extensions for <see cref="IQuartzBuilder"/> that apply the family's Quartz store configuration.
/// </summary>
/// <remarks>
/// Kept as an <see cref="IQuartzBuilder"/> extension (not a hidden <c>Add…</c> facade) so the host's own
/// <c>AddQuartz</c> call stays visible in its <c>Program</c> while the policy lives here — the same shape
/// the <c>Http</c> options extensions use.
/// </remarks>
public static class QuartzBuilderExtensions
{
    /// <summary>
    /// Applies the clustered Postgres store every host in the family runs: the machine name as the
    /// instance id, a simple type loader, the ADO store on Postgres with job data stored as strings, and
    /// the System.Text.Json serializer.
    /// </summary>
    /// <param name="builder">The Quartz builder to configure.</param>
    /// <param name="instance">
    /// The scheduler's name — the identity its rows carry in the store, so two schedulers sharing a
    /// database stay apart (e.g. <c>retry</c> for a bus retry store, the app's own name for a job host).
    /// </param>
    /// <param name="connection">
    /// The <b>name</b> of the connection string, resolved by Quartz from the host's
    /// <c>ConnectionStrings</c> section. A name that does not resolve fails the scheduler's start with a
    /// <c>SchedulerConfigException</c> naming it, so the host does not come up with a store it cannot reach.
    /// </param>
    /// <param name="schema">
    /// The schema holding the store tables, without a trailing dot — it is appended here, so a caller
    /// cannot forget it and end up pointing at another schema. Note that this is Quartz's
    /// <c>TablePrefix</c>, which <b>replaces</b> its <c>QRTZ_</c> default rather than adding to it: with
    /// <c>bus</c> the store reads <c>bus.TRIGGERS</c> and <c>bus.JOB_DETAILS</c>, not <c>bus.QRTZ_TRIGGERS</c>.
    /// The schema itself is not created: <c>ProvisionSchema()</c> creates the tables, never the schema
    /// holding them, and Quartz 4 validates their presence when the scheduler starts.
    /// </param>
    /// <returns>The same <paramref name="builder"/>, for chaining.</returns>
    /// <exception cref="System.ArgumentException">
    /// Thrown when the scheduler starts and the host has not referenced <c>Npgsql</c>: Quartz resolves
    /// its ADO provider by name at runtime, and this package depends on <c>Quartz</c> alone.
    /// </exception>
    /// <remarks>
    /// Clustering is on, which is what lets several replicas share one store without firing a trigger
    /// twice; it requires the rows' instance ids to differ, hence the machine name. Job data is stored as
    /// strings rather than a serialized blob — the setting Quartz 3 called <c>UseProperties</c> — so the
    /// tables stay readable and a payload written by one version can be read by the next.
    /// </remarks>
    public static IQuartzBuilder WithPostgresDefaults(this IQuartzBuilder builder, string instance, string connection, string schema)
    {
        builder
            .ConfigureScheduler(options =>
            {
                options.InstanceId = Environment.MachineName;
                options.InstanceName = instance;
            })
            .UseSimpleTypeLoader()
            .UsePersistentStore(store =>
                store
                    .UsePostgres(options => options.ConnectionStringName = connection)
                    .ConfigureStore(options =>
                    {
                        options.TablePrefix = $"{schema}.";
                        options.StoreJobDataAsStrings = true;
                    })
                    .UseClustering()
                    .UseSystemTextJsonSerializer()
            );

        return builder;
    }
}
