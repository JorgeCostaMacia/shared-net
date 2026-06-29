using FluentValidation.Results;
using JorgeCostaMacia.Exception.Domain;
using System.Collections.Immutable;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents a domain exception thrown when one or more constraints fail validation
/// on a <c>LongValueObject</c> or any derived Value Object.
/// </summary>
/// <remarks>
/// <para>
/// This exception inherits from <see cref="ValidationException"/> and carries the specific validation
/// failures from FluentValidation, with a consistent error code and self-identifying aggregate type.
/// </para>
/// </remarks>
public class LongValueObjectValidationException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LongValueObjectValidationException"/> class with full context information.
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate that caused the exception.</param>
    /// <param name="aggregateType">The type identifier of the aggregate (e.g., entity name).</param>
    /// <param name="aggregateCode">A specific code identifying the exception type (often a unique GUID).</param>
    /// <param name="aggregateHttpCode">The recommended HTTP status code for this exception type.</param>
    /// <param name="aggregateOccurredAt">The UTC date and time the exception occurred.</param>
    /// <param name="message">A user-friendly description of the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    /// <param name="validations">A list of specific validation failures associated with the constraint violation.</param>
    public LongValueObjectValidationException(
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
    /// Initializes a new instance of the <see cref="LongValueObjectValidationException"/> class from the given validation failures, applying the default error code for this type.
    /// </summary>
    /// <param name="validations">The validation failures that triggered this exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    public LongValueObjectValidationException(
        IEnumerable<ValidationFailure> validations,
        System.Exception? innerException = null
    ) : base(
        null,
        typeof(LongValueObjectValidationException).FullName ?? typeof(LongValueObjectValidationException).Name,
#if NET9_0_OR_GREATER
    new Guid("00a6307a-a9aa-7e5a-9f7f-4c223fa2c0c8")
#else
    new Guid("c3b6a04a-3152-4615-88cf-464e62bb8ce4")
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
