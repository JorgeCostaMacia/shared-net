namespace JorgeCostaMacia.Exception.Domain;

/// <summary>
/// Provides default static values for the <see cref="NotFoundException"/> hierarchy.
/// </summary>
/// <remarks>
/// <para>
/// This class centralizes default constants used when initializing properties of exceptions
/// that signal that a requested entity or resource could not be found.
/// </para>
///
/// <para>Properties provide default values for:</para>
/// <list type="bullet">
///    <item><description><see cref="AGGREGATE_TYPE"/>: The full type name of the base <see cref="NotFoundException"/>.</description></item>
///    <item><description><see cref="AGGREGATE_HTTP_CODE"/>: The default HTTP status code (404) for resource not found errors.</description></item>
///    <item><description><see cref="AGGREGATE_CODE"/>: The fixed code for unclassified not found errors.</description></item>
/// </list>
/// </remarks>
public static class NotFoundExceptionDefaults
{
    /// <summary>
    /// The default full type name, used as a fallback for <see cref="DomainException.AggregateType"/>.
    /// </summary>
    public static string AGGREGATE_TYPE => typeof(NotFoundException).FullName ?? typeof(NotFoundException).Name;

    /// <summary>
    /// The default HTTP status code for resource not found domain errors (404 Not Found).
    /// </summary>
    public static int AGGREGATE_HTTP_CODE => 404;

    /// <summary>
    /// The default fixed unique identifier representing a generic Not Found error.
    /// This constant allows grouping of all unspecified <see cref="NotFoundException"/> instances under a single code for logging and monitoring.
    /// </summary>
    public static Guid AGGREGATE_CODE =>
#if NET9_0_OR_GREATER
    new Guid("019ab701-befa-7735-bae0-1587c5504777");
#else
    new Guid("a7348a55-d8d8-48f9-b167-e96aa7e16337");
#endif
}