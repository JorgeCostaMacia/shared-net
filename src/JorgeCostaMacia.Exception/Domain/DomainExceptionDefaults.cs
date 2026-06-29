namespace JorgeCostaMacia.Exception.Domain;

/// <summary>
/// Provides default static constants/fallback values for the <see cref="DomainException"/> hierarchy.
/// </summary>
/// <remarks>
/// <para>
/// This class centralizes fixed default values (such as HTTP codes and error type codes) used when
/// specific values are not provided during exception construction.
/// </para>
/// <para>
/// Note: Dynamic values (like unique Instance IDs or Timestamps) are purposefully excluded here
/// and should be generated within the exception constructor to ensure correctness per instance.
/// </para>
/// <para>Available defaults:</para>
/// <list type="bullet">
///    <item><description><see cref="AGGREGATE_TYPE"/>: Provides the full type name of the base <see cref="DomainException"/>.</description></item>
///    <item><description><see cref="AGGREGATE_HTTP_CODE"/>: The default HTTP status code (500) for general domain exceptions.</description></item>
///    <item><description><see cref="AGGREGATE_CODE"/>: The fixed, default error code for unclassified domain errors.</description></item>
/// </list>
/// </remarks>
public static class DomainExceptionDefaults
{
    /// <summary>
    /// The default full type name, used as a fallback for <see cref="DomainException.AggregateType"/>.
    /// </summary>
    public static string AGGREGATE_TYPE => typeof(DomainException).FullName ?? typeof(DomainException).Name;

    /// <summary>
    /// The default HTTP status code for general, unclassified domain errors (500 Internal Server Error).
    /// </summary>
    public static int AGGREGATE_HTTP_CODE => 500;

    /// <summary>
    /// The default fixed unique identifier representing a generic Domain Error.
    /// This constant allows grouping of all unspecified domain exceptions under a single code.
    /// </summary>
    public static Guid AGGREGATE_CODE =>
#if NET9_0_OR_GREATER
    new Guid("019ab6e5-2892-731e-84b6-028973d9a20a");
#else
    new Guid("59f1d620-5763-4045-81ef-440061d162d8");
#endif
}