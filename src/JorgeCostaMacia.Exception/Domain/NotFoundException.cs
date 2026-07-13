
namespace JorgeCostaMacia.Exception.Domain;

/// <summary>
/// Represents a domain exception that occurs when an entity or resource does not exist.
/// Commonly used to signal missing data or lookup failures.
/// </summary>
/// <remarks>
/// <para>
/// Inherit from this class for exceptions that indicate a <b>requested entity or resource was not found</b>
/// within the system, such as failed lookups by ID or key.
/// </para>
///
/// <para>Examples of usage include:</para>
/// <list type="bullet">
///    <item><description>Retrieving a user by ID when the user does not exist.</description></item>
///    <item><description>Querying for a product by SKU that is not found in the catalog.</description></item>
/// </list>
///
/// <para>Properties include:</para>
/// <list type="bullet">
///    <item><description>Inherited metadata from <see cref="DomainException"/>: <see cref="DomainException.AggregateId"/>, <see cref="DomainException.AggregateType"/>, <see cref="DomainException.AggregateCode"/>, <see cref="DomainException.AggregateHttpCode"/>, and <see cref="DomainException.AggregateOccurredAt"/>.</description></item>
/// </list>
///
/// <para>
/// Constructors allow automatic generation of metadata (IDs, timestamps, etc.) and primarily use
/// a default HTTP status code of <b>404 (Not Found)</b>, thanks to <see cref="NotFoundExceptionDefaults"/>.
/// </para>
/// </remarks>
public abstract class NotFoundException : DomainException
{
    /// <summary>
    /// Constructor with explicit metadata for resource not found exceptions.
    /// Provides full control over all base exception properties and delegates initialization to the base <see cref="DomainException"/> constructor.
    /// </summary>
    /// <param name="aggregateId">Unique identifier for this exception instance. (Required)</param>
    /// <param name="aggregateType">Full type name of the exception. (Required)</param>
    /// <param name="aggregateCode">Unique code representing the specific domain error. (Required)</param>
    /// <param name="aggregateHttpCode">HTTP status code associated with this error (e.g., 404).</param>
    /// <param name="aggregateOccurredAt">Timestamp of when this exception occurred.</param>
    /// <param name="message">Optional exception message passed to the base <see cref="System.Exception"/>.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    protected NotFoundException(
        Guid aggregateId,
        string aggregateType,
        Guid aggregateCode,
        int aggregateHttpCode,
        DateTime aggregateOccurredAt,
        string? message,
        System.Exception? innerException
    ) : base(
        aggregateId,
        aggregateType,
        aggregateCode,
        aggregateHttpCode,
        aggregateOccurredAt,
        message,
        innerException
    )
    { }

    /// <summary>
    /// Constructor with optional metadata, automatically generating IDs and using 'Not Found' defaults.
    /// The default <see cref="DomainException.AggregateHttpCode"/> is set to 404 Not Found.
    /// </summary>
    /// <param name="aggregateId">Optional unique identifier of this exception instance. Defaults to a new GUID using <see cref="GuidFactory.Domain.GuidFactory.Create()"/> if null.</param>
    /// <param name="aggregateType">Optional full type name of the exception. Defaults to <see cref="NotFoundExceptionDefaults.AGGREGATE_TYPE"/> if null.</param>
    /// <param name="aggregateCode">Optional unique code representing the specific domain error. Defaults to the fixed GUID from <see cref="NotFoundExceptionDefaults.AGGREGATE_CODE"/> if null.</param>
    /// <param name="aggregateHttpCode">Optional HTTP status code associated with this error. Defaults to 404 (Not Found) from <see cref="NotFoundExceptionDefaults.AGGREGATE_HTTP_CODE"/> if null.</param>
    /// <param name="aggregateOccurredAt">Optional timestamp of when this exception occurred. Defaults to <see cref="DateTime.UtcNow"/> if null.</param>
    /// <param name="message">Optional exception message. Delegates to the base message logic defined in <see cref="DomainException"/>.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    protected NotFoundException(
        Guid? aggregateId,
        string? aggregateType,
        Guid? aggregateCode,
        int? aggregateHttpCode,
        DateTime? aggregateOccurredAt,
        string? message,
        System.Exception? innerException
    ) : base(
        aggregateId,
        aggregateType ?? NotFoundExceptionDefaults.AGGREGATE_TYPE,
        aggregateCode ?? NotFoundExceptionDefaults.AGGREGATE_CODE,
        aggregateHttpCode ?? NotFoundExceptionDefaults.AGGREGATE_HTTP_CODE,
        aggregateOccurredAt,
        message,
        innerException
    )
    { }
}
