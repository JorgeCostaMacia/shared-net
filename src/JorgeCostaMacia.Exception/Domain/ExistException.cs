
namespace JorgeCostaMacia.Exception.Domain;

/// <summary>
/// Represents a domain exception that occurs when an entity or resource already exists.
/// Commonly used to signal duplicate key, uniqueness, or conflict violations.
/// </summary>
/// <remarks>
/// <para>
/// Inherit from this class for exceptions that indicate the attempted creation or registration
/// of an entity that already exists within the system, such as duplicate entries or unique constraint conflicts.
/// </para>
///
/// <para>Examples of usage include:</para>
/// <list type="bullet">
///    <item><description>Creating a user with an email that already exists.</description></item>
///    <item><description>Registering a product with a duplicate SKU.</description></item>
/// </list>
///
/// <para>Properties include:</para>
/// <list type="bullet">
///    <item><description>Inherited metadata from <see cref="DomainException"/>: <see cref="DomainException.AggregateId"/>, <see cref="DomainException.AggregateType"/>, <see cref="DomainException.AggregateCode"/>, <see cref="DomainException.AggregateHttpCode"/>, and <see cref="DomainException.AggregateOccurredAt"/>.</description></item>
/// </list>
///
/// <para>
/// Constructors allow automatic generation of metadata (IDs, timestamps, etc.) and primarily use
/// a default HTTP status code of 409 (Conflict), thanks to <see cref="ExistExceptionDefaults"/>.
/// </para>
/// </remarks>
public abstract class ExistException : DomainException
{
    /// <summary>
    /// Constructor with explicit metadata for existence conflict exceptions.
    /// Provides full control over all base exception properties and delegates initialization to the base <see cref="DomainException"/> constructor.
    /// </summary>
    /// <param name="aggregateId">Unique identifier for this exception instance. (Required)</param>
    /// <param name="aggregateType">Full type name of the exception. (Required)</param>
    /// <param name="aggregateCode">Unique code representing the specific domain error. (Required)</param>
    /// <param name="aggregateHttpCode">HTTP status code associated with this error (e.g., 409).</param>
    /// <param name="aggregateOccurredAt">Timestamp of when this exception occurred.</param>
    /// <param name="message">Optional exception message passed to the base <see cref="System.Exception"/>.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    protected ExistException(
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
    /// Constructor with optional metadata, automatically generating IDs and using existence conflict defaults.
    /// The default <see cref="DomainException.AggregateHttpCode"/> is set to 409 Conflict.
    /// </summary>
    /// <param name="aggregateId">Optional unique identifier of this exception instance. Defaults to a new GUID using <see cref="GuidFactory.Domain.GuidFactory.Create()"/>.</param>
    /// <param name="aggregateType">Optional full type name of the exception. Defaults to <see cref="ExistExceptionDefaults.AGGREGATE_TYPE"/> if null.</param>
    /// <param name="aggregateCode">Optional unique code representing the specific domain error. Defaults to the fixed GUID from <see cref="ExistExceptionDefaults.AGGREGATE_CODE"/> if null.</param>
    /// <param name="aggregateHttpCode">Optional HTTP status code associated with this error. Defaults to 409 (Conflict) from <see cref="ExistExceptionDefaults.AGGREGATE_HTTP_CODE"/> if null.</param>
    /// <param name="aggregateOccurredAt">Optional timestamp of when this exception occurred. Defaults to <see cref="DateTime.UtcNow"/> if null.</param>
    /// <param name="message">Optional exception message. Delegates to the base message logic defined in <see cref="DomainException"/>.</param>
    /// <param name="innerException">Inner exception, if any.</param>
    protected ExistException(
        Guid? aggregateId,
        string? aggregateType,
        Guid? aggregateCode,
        int? aggregateHttpCode,
        DateTime? aggregateOccurredAt,
        string? message,
        System.Exception? innerException
    ) : base(
        aggregateId,
        aggregateType ?? ExistExceptionDefaults.AGGREGATE_TYPE,
        aggregateCode ?? ExistExceptionDefaults.AGGREGATE_CODE,
        aggregateHttpCode ?? ExistExceptionDefaults.AGGREGATE_HTTP_CODE,
        aggregateOccurredAt,
        message,
        innerException
    )
    { }
}
