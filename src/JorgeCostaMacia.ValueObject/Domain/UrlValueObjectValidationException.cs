using FluentValidation.Results;
using System.Collections.Immutable;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents a domain exception thrown when one or more constraints fail validation
/// on a <see cref="UrlValueObject"/>.
/// </summary>
/// <remarks>
/// <para>
/// This exception inherits from <see cref="StringValueObjectValidationException"/> and is used to signal
/// a violation of the domain's business rules specific to URL/URI validation (e.g., malformed format, unsupported protocol).
/// It carries the specific validation failures from FluentValidation.
/// </para>
/// <para>
/// It provides specialized constructors for internal use, ensuring a consistent error code
/// and self-identifying aggregate type for this specific constraint violation.
/// </para>
/// </remarks>
public class UrlValueObjectValidationException : StringValueObjectValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UrlValueObjectValidationException"/> class with full context information.
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate that caused the exception.</param>
    /// <param name="aggregateType">The type identifier of the aggregate (e.g., entity name).</param>
    /// <param name="aggregateCode">A specific code identifying the exception type (often a unique GUID).</param>
    /// <param name="aggregateHttpCode">The recommended HTTP status code for this exception type.</param>
    /// <param name="aggregateOccurredAt">The UTC date and time the exception occurred.</param>
    /// <param name="message">A user-friendly description of the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    /// <param name="validations">A list of specific validation failures associated with the constraint violation.</param>
    public UrlValueObjectValidationException(
        Guid aggregateId,
        string aggregateType,
        Guid aggregateCode,
        int aggregateHttpCode,
        DateTime aggregateOccurredAt,
        string? message,
        System.Exception? innerException,
        ImmutableList<ValidationFailure> validations
    ) : base(
        aggregateId,
        aggregateType,
        aggregateCode,
        aggregateHttpCode,
        aggregateOccurredAt,
        message,
        innerException,
        validations
    )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="UrlValueObjectValidationException"/> class from the given validation failures, applying the default error code for this type.
    /// </summary>
    /// <param name="validations">The validation failures that triggered this exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    public UrlValueObjectValidationException(
        IEnumerable<ValidationFailure> validations,
        System.Exception? innerException = null
    ) : base(
        null,
        typeof(UrlValueObjectValidationException).FullName ?? typeof(UrlValueObjectValidationException).Name,
#if NET9_0_OR_GREATER
    new Guid("01951f44-0397-7ea0-8872-ef9b2ba6bfb1")
#else
    new Guid("fd09dd89-0e91-4b32-bc84-630c7a63c986")
#endif
        ,
        null,
        null,
        null,
        innerException,
        validations
    )
    { }
}