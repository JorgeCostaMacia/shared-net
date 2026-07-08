
namespace JorgeCostaMacia.Exception.Domain;

/// <summary>
/// Base exception type for domain-specific errors.
/// Provides metadata for tracing, auditing, and correlation across systems.
/// </summary>
/// <remarks>
/// <para>
/// This abstract class is intended to be inherited by all exceptions representing
/// business or domain rule violations within the system. It extends the standard
/// <see cref="System.Exception"/> class by embedding essential traceability information (metadata)
/// directly into the exception object.
/// </para>
///
/// <para>Properties include:</para>
/// <list type="bullet">
///   <item><description><see cref="AggregateId"/>: Unique identifier for this exception instance, for tracing and correlation.</description></item>
///   <item><description><see cref="AggregateType"/>: Full name of the exception type (including namespace).</description></item>
///   <item><description><see cref="AggregateCode"/>: Unique code representing the specific domain error.</description></item>
///   <item><description><see cref="AggregateHttpCode"/>: HTTP status code associated with this error. In APIs, this may be used as the response status; in background services it serves as metadata.</description></item>
///   <item><description><see cref="AggregateOccurredAt"/>: Timestamp when the exception instance occurred.</description></item>
/// </list>
///
/// <para>
/// Constructors allow automatic generation of IDs, type names, and timestamps if not provided,
/// ensuring consistent logging and correlation across services and logs. The exception message itself
/// is automatically formatted to include key metadata for better readability in plain text logs (e.g., ID/Type => Message).
/// </para>
/// </remarks>
public abstract class DomainException : System.Exception
{
    /// <summary>
    /// Unique identifier for this particular exception instance.
    /// Useful for tracing and correlating errors in logs or monitoring systems.
    /// </summary>
    public Guid AggregateId { get; init; }

    /// <summary>
    /// Full name of the aggregate type associated with this exception.
    /// Typically the full type name of the derived exception class.
    /// </summary>
    public string AggregateType { get; init; }

    /// <summary>
    /// Unique identifier representing the specific type of domain error.
    /// Can be used for internal mapping or categorization.
    /// </summary>
    public Guid AggregateCode { get; init; }

    /// <summary>
    /// HTTP status code associated with this error.
    /// In the context of an API, this code can be used as the response status.
    /// In background services or non-HTTP contexts, it serves purely as metadata for reference.
    /// </summary>
    public int AggregateHttpCode { get; init; }

    /// <summary>
    /// Timestamp in UTC indicating when this exception instance occurred.
    /// </summary>
    public DateTime AggregateOccurredAt { get; init; }

    /// <summary>
    /// Constructor that explicitly sets all metadata fields, providing full control over the exception's context and traceability.
    /// </summary>
    /// <param name="aggregateId">Unique identifier for this exception instance. Cannot be null.</param>
    /// <param name="aggregateType">Full type name of the exception, typically the derived class name.</param>
    /// <param name="aggregateCode">Unique code representing the specific type of domain error.</param>
    /// <param name="aggregateHttpCode">HTTP status code associated with this error.</param>
    /// <param name="aggregateOccurredAt">Timestamp in UTC indicating when the exception instance occurred.</param>
    /// <param name="message">Optional exception message passed to the base <see cref="System.Exception"/>.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    protected DomainException(
        Guid aggregateId,
        string aggregateType,
        Guid aggregateCode,
        int aggregateHttpCode,
        DateTime aggregateOccurredAt,
        string? message,
        System.Exception? innerException
    ) : base(message, innerException)
    {
        AggregateId = aggregateId;
        AggregateType = aggregateType;
        AggregateCode = aggregateCode;
        AggregateHttpCode = aggregateHttpCode;
        AggregateOccurredAt = aggregateOccurredAt;
    }

    /// <summary>
    /// Constructor that allows optional metadata fields.
    /// Automatically generates IDs, timestamps, and default codes if not provided,
    /// leveraging the <see cref="DomainExceptionDefaults"/> class for constants and <see cref="JorgeCostaMacia.GuidFactory.Domain.GuidFactory"/> for dynamic values.
    /// </summary>
    /// <remarks>
    /// When <paramref name="aggregateId"/> is <c>null</c>, the generated <see cref="Guid"/> is
    /// computed once while building the formatted exception message and reused for the
    /// <see cref="AggregateId"/> property, ensuring both always refer to the exact same identifier
    /// rather than two independently generated values.
    /// </remarks>
    /// <param name="aggregateId">Optional unique identifier of this exception instance. <b>Generates a new GUID</b> using <see cref="JorgeCostaMacia.GuidFactory.Domain.GuidFactory.Create()"/> if null.</param>
    /// <param name="aggregateType">Optional full type name of the exception. Defaults to <see cref="DomainExceptionDefaults.AGGREGATE_TYPE"/> (the full name of the base class) if null.</param>
    /// <param name="aggregateCode">Optional unique code representing the specific domain error. Defaults to the <b>fixed GUID</b> defined in <see cref="DomainExceptionDefaults.AGGREGATE_CODE"/> if null.</param>
    /// <param name="aggregateHttpCode">Optional HTTP status code associated with this error. Defaults to the constant <see cref="DomainExceptionDefaults.AGGREGATE_HTTP_CODE"/> (500) if null.</param>
    /// <param name="aggregateOccurredAt">Optional timestamp of when this exception occurred. Defaults to <see cref="DateTime.UtcNow"/> if null.</param>
    /// <param name="message">Optional exception message. If provided, it is appended to the metadata using the "=>" separator.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    protected DomainException(
        Guid? aggregateId,
        string? aggregateType,
        Guid? aggregateCode,
        int? aggregateHttpCode,
        DateTime? aggregateOccurredAt,
        string? message,
        System.Exception? innerException
    ) : base($"{aggregateId ?? (aggregateId = JorgeCostaMacia.GuidFactory.Domain.GuidFactory.Create())}/{(aggregateType ?? DomainExceptionDefaults.AGGREGATE_TYPE).Split('.').Last()}{(string.IsNullOrEmpty(message) ? string.Empty : " => " + message?.Trim())}", innerException)
    {
        AggregateId = aggregateId ?? JorgeCostaMacia.GuidFactory.Domain.GuidFactory.Create();
        AggregateType = aggregateType ?? DomainExceptionDefaults.AGGREGATE_TYPE;
        AggregateCode = aggregateCode ?? DomainExceptionDefaults.AGGREGATE_CODE;
        AggregateHttpCode = aggregateHttpCode ?? DomainExceptionDefaults.AGGREGATE_HTTP_CODE;
        AggregateOccurredAt = aggregateOccurredAt ?? DateTime.UtcNow;
    }
}
