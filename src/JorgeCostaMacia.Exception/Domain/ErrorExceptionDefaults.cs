namespace JorgeCostaMacia.Exception.Domain;

/// <summary>
/// Provides default static values for the <see cref="ErrorException"/> hierarchy.
/// </summary>
/// <remarks>
/// <para>
/// This class centralizes default constants used when initializing properties of exceptions
/// that capture aggregated business errors, ensuring consistency and readability in constructors.
/// </para>
///
/// <para>Properties provide default values for:</para>
/// <list type="bullet">
///    <item><description><see cref="AGGREGATE_TYPE"/>: The full type name of the base <see cref="ErrorException"/>.</description></item>
///    <item><description><see cref="AGGREGATE_HTTP_CODE"/>: The default HTTP status code (400) for general, aggregated client errors.</description></item>
///    <item><description><see cref="AGGREGATE_CODE"/>: The fixed code for unclassified aggregation errors.</description></item>
/// </list>
/// </remarks>
public static class ErrorExceptionDefaults
{
    /// <summary>
    /// The default full type name, used as a fallback for <see cref="DomainException.AggregateType"/>.
    /// </summary>
    public static string AGGREGATE_TYPE => typeof(ErrorException).FullName ?? typeof(ErrorException).Name;

    /// <summary>
    /// The default HTTP status code for general, client-side aggregated domain errors (400 Bad Request).
    /// </summary>
    public static int AGGREGATE_HTTP_CODE => 400;

    /// <summary>
    /// The default fixed unique identifier representing a generic Aggregated Domain Error.
    /// This constant allows grouping of all unspecified <see cref="ErrorException"/> instances under a single code for logging and monitoring.
    /// </summary>
    public static Guid AGGREGATE_CODE =>
#if NET9_0_OR_GREATER
    new Guid("019ab6fc-8bb5-7c62-ad6f-b5062c8d5575");
#else
    new Guid("952fff75-0db0-46f1-8f82-b108e001f03e");
#endif
}