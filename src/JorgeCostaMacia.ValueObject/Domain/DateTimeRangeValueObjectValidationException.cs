using System.Collections.Immutable;
using FluentValidation.Results;
using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents a domain exception thrown when one or more constraints fail validation
/// on a <c>DateTimeRangeValueObject</c> or any derived Value Object.
/// </summary>
/// <remarks>
/// <para>
/// This exception inherits from <see cref="ValidationException"/> and is used to signal
/// a violation of the domain's business rules related to date range Value Objects (e.g., start date is after end date).
/// It carries the specific validation failures from FluentValidation.
/// </para>
/// <para>
/// It provides specialized constructors for internal use, ensuring a consistent error code
/// and self-identifying aggregate type for this constraint violation.
/// </para>
/// </remarks>
public class DateTimeRangeValueObjectValidationException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeRangeValueObjectValidationException"/> class with full context information.
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate that caused the exception.</param>
    /// <param name="aggregateType">The type identifier of the aggregate (e.g., entity name).</param>
    /// <param name="aggregateCode">A specific code identifying the exception type (often a unique GUID).</param>
    /// <param name="aggregateHttpCode">The recommended HTTP status code for this exception type.</param>
    /// <param name="aggregateOccurredAt">The UTC date and time the exception occurred.</param>
    /// <param name="message">A user-friendly description of the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    /// <param name="validations">A list of specific validation failures associated with the constraint violation.</param>
    public DateTimeRangeValueObjectValidationException(
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
    /// Initializes a new instance of the <see cref="DateTimeRangeValueObjectValidationException"/> class from the given validation failures, applying the default error code for this type.
    /// </summary>
    /// <param name="validations">The validation failures that triggered this exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    public DateTimeRangeValueObjectValidationException(
        IEnumerable<ValidationFailure> validations,
        System.Exception? innerException = null
    ) : base(
        null,
        typeof(DateTimeRangeValueObjectValidationException).FullName ?? typeof(DateTimeRangeValueObjectValidationException).Name,
#if NET9_0_OR_GREATER
    new Guid("01951f26-fd3e-7370-be17-d590fb9e54b9")
#else
    new Guid("a9a32cd8-4197-4c59-8abf-8ae7f644cdd5")
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