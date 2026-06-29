namespace JorgeCostaMacia.Exception.Domain;

/// <summary>
/// Provides default static values for the <see cref="ValidationException"/> hierarchy.
/// </summary>
/// <remarks>
/// <para>
/// This class centralizes default constants used when initializing properties of exceptions
/// that signal that one or more validation constraints were violated.
/// </para>
///
/// <para>Properties provide default values for:</para>
/// <list type="bullet">
///    <item><description><see cref="AGGREGATE_TYPE"/>: The full type name of the base <see cref="ValidationException"/>.</description></item>
///    <item><description><see cref="AGGREGATE_HTTP_CODE"/>: The default HTTP status code (400) for client-side validation errors.</description></item>
///    <item><description><see cref="AGGREGATE_CODE"/>: The fixed code for unclassified validation errors.</description></item>
/// </list>
/// </remarks>
public static class ValidationExceptionDefaults
{
    /// <summary>
    /// The default full type name, used as a fallback for <see cref="DomainException.AggregateType"/>.
    /// </summary>
    public static string AGGREGATE_TYPE => typeof(ValidationException).FullName ?? typeof(ValidationException).Name;

    /// <summary>
    /// The default HTTP status code for client-side validation constraint domain errors (400 Bad Request).
    /// </summary>
    public static int AGGREGATE_HTTP_CODE => 400;

    /// <summary>
    /// The default fixed unique identifier representing a generic Validation Error.
    /// This constant allows grouping of all unspecified <see cref="ValidationException"/> instances under a single code for logging and monitoring.
    /// </summary>
    public static Guid AGGREGATE_CODE =>
#if NET9_0_OR_GREATER
    new Guid("019ab702-1cb1-7a71-a8f0-2f789e830fb5");
#else
    new Guid("efa5e0c0-cfad-4941-90bf-df535bd344f2");
#endif
}