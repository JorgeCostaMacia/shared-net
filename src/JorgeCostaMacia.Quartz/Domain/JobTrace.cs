using Quartz;

namespace JorgeCostaMacia.Quartz.Domain;

/// <summary>
/// The trace identity of a Quartz job execution: the <see cref="AggregateId"/>/<see cref="CorrelationId"/>
/// pair every observer of the same execution shares.
/// </summary>
/// <remarks>
/// <para>
/// <see cref="GetOrCreate(IJobExecutionContext)"/> is the single place where the pair is minted and
/// stored: it reads the identifiers from the execution context, creates the missing ones, and puts
/// them back — so job listeners, trigger listeners, event publishers and the job itself all converge
/// on the same identifiers <b>regardless of who runs first</b>. The get-or-create is idempotent:
/// there is no registration-order contract between observers.
/// </para>
/// <para>
/// The identifiers live in the context under the <c>AggregateId</c>/<c>CorrelationId</c> keys, the
/// same names the rest of the <c>JorgeCostaMacia.*</c> ecosystem uses for message and log correlation.
/// </para>
/// </remarks>
public sealed record JobTrace
{
    private const string AGGREGATE_ID_KEY = "AggregateId";
    private const string CORRELATION_ID_KEY = "CorrelationId";

    /// <summary>
    /// Unique identifier of this execution's subject, minted by the GuidFactory
    /// (UUIDv7 on .NET 9+, UUIDv4 on .NET 8) when absent from the context.
    /// </summary>
    public Guid AggregateId { get; init; }

    /// <summary>
    /// Identifier correlating this execution with the wider workflow; defaults to
    /// <see cref="AggregateId"/> when absent from the context.
    /// </summary>
    public Guid CorrelationId { get; init; }

    /// <summary>Initializes the trace with the given identifiers.</summary>
    /// <param name="aggregateId">The execution subject's unique identifier.</param>
    /// <param name="correlationId">The workflow correlation identifier.</param>
    public JobTrace(Guid aggregateId, Guid correlationId)
    {
        AggregateId = aggregateId;
        CorrelationId = correlationId;
    }

    /// <summary>
    /// Mints a fresh trace for observers that have no execution context to share it through —
    /// e.g. a trigger misfire, which never reached execution. The correlation defaults to the
    /// aggregate id.
    /// </summary>
    /// <returns>A new, unshared <see cref="JobTrace"/>.</returns>
    public static JobTrace Create()
    {
        Guid aggregateId = GuidFactory.Domain.GuidFactory.Create();

        return new JobTrace(aggregateId, aggregateId);
    }

    /// <summary>
    /// Reads the trace identifiers from the execution context, minting the missing ones, and puts
    /// them back so every later reader of the same execution gets the same pair. Idempotent —
    /// calling it from several listeners in any order always converges on the first minted values.
    /// </summary>
    /// <param name="context">The job execution context the trace travels in.</param>
    /// <returns>The execution's <see cref="JobTrace"/>.</returns>
    public static JobTrace GetOrCreate(IJobExecutionContext context)
    {
        Guid aggregateId = context.Get(AGGREGATE_ID_KEY) is Guid id ? id : GuidFactory.Domain.GuidFactory.Create();
        Guid correlationId = context.Get(CORRELATION_ID_KEY) is Guid correlation ? correlation : aggregateId;

        context.Put(AGGREGATE_ID_KEY, aggregateId);
        context.Put(CORRELATION_ID_KEY, correlationId);

        return new JobTrace(aggregateId, correlationId);
    }
}
