using FluentValidation.Results;
using JorgeCostaMacia.Exception.Domain;
using System.Collections.Immutable;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents a domain exception thrown when one or more constraints fail validation
/// on a <see cref="UuidValueObject"/> or any derived Value Object.
/// </summary>
/// <remarks>
/// <para>
/// This exception inherits from <see cref="ValidationException"/> and is used to signal
/// a violation of the domain's business rules related to GUID/UUID-based Value Objects (e.g., empty GUID, invalid format during conversion).
/// It carries the specific validation failures from FluentValidation.
/// </para>
/// <para>
/// It provides specialized constructors for internal use, ensuring a consistent error code
/// and self-identifying aggregate type for this constraint violation.
/// </para>
/// </remarks>
public class UuidValueObjectValidationException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UuidValueObjectValidationException"/> class with full context information.
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate that caused the exception.</param>
    /// <param name="aggregateType">The type identifier of the aggregate (e.g., entity name).</param>
    /// <param name="aggregateCode">A specific code identifying the exception type (often a unique GUID).</param>
    /// <param name="aggregateHttpCode">The recommended HTTP status code for this exception type.</param>
    /// <param name="aggregateOccurredAt">The UTC date and time the exception occurred.</param>
    /// <param name="message">A user-friendly description of the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    /// <param name="validations">A list of specific validation failures associated with the constraint violation.</param>
    public UuidValueObjectValidationException(
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
    /// Initializes a new instance of the <see cref="UuidValueObjectValidationException"/> class from the given validation failures, applying the default error code for this type.
    /// </summary>
    /// <param name="validations">The validation failures that triggered this exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    public UuidValueObjectValidationException(
        IEnumerable<ValidationFailure> validations,
        System.Exception? innerException = null
    ) : base(
        null,
        typeof(UuidValueObjectValidationException).FullName ?? typeof(UuidValueObjectValidationException).Name,
#if NET9_0_OR_GREATER
    new Guid("01951f44-5081-7a24-a1c8-fcd7b5d9f1ed")
#else
    new Guid("9a6d9202-fa4b-4229-8c2e-28f2768eaa63")
#endif
        ,
        null,
        null,
        null,
        innerException,
        validations)
    { }
}