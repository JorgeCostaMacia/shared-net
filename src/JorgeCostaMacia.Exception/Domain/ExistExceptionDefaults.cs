namespace JorgeCostaMacia.Exception.Domain;

/// <summary>
/// Provides default static values for the <see cref="ExistException"/> hierarchy.
/// </summary>
/// <remarks>
/// <para>
/// This class centralizes default constants used when initializing properties of exceptions
/// that signal the attempted creation or registration of an entity that already exists.
/// </para>
///
/// <para>Properties provide default values for:</para>
/// <list type="bullet">
///    <item><description><see cref="AGGREGATE_TYPE"/>: The full type name of the base <see cref="ExistException"/>.</description></item>
///    <item><description><see cref="AGGREGATE_HTTP_CODE"/>: The default HTTP status code (409) for resource conflict errors.</description></item>
///    <item><description><see cref="AGGREGATE_CODE"/>: The fixed code for unclassified existence errors.</description></item>
/// </list>
/// </remarks>
public static class ExistExceptionDefaults
{
    /// <summary>
    /// The default full type name, used as a fallback for <see cref="DomainException.AggregateType"/>.
    /// </summary>
    public static string AGGREGATE_TYPE => typeof(ExistException).FullName ?? typeof(ExistException).Name;

    /// <summary>
    /// The default HTTP status code for resource conflict domain errors (409 Conflict).
    /// </summary>
    public static int AGGREGATE_HTTP_CODE => 409;

    /// <summary>
    /// The default fixed unique identifier representing a generic existence conflict error.
    /// This constant allows grouping of all unspecified <see cref="ExistException"/> instances under a single code for logging and monitoring.
    /// </summary>
    public static Guid AGGREGATE_CODE =>
#if NET9_0_OR_GREATER
    new Guid("019ab6ff-ffdc-7cb7-8b57-8bddf0f63806");
#else
    new Guid("98f2b2e0-beee-4743-ab26-96f5cb8351be");
#endif
}