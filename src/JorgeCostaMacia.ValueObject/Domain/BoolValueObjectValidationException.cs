using System.Collections.Immutable;
using FluentValidation.Results;
using JorgeCostaMacia.Exception.Domain;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents a domain exception thrown when one or more constraints fail validation
/// on a <see cref="BoolValueObject"/> or any derived Value Object.
/// </summary>
/// <remarks>
/// <para>
/// This exception inherits from <see cref="ValidationException"/> and is used to signal
/// a violation of the domain's business rules related to Boolean-based Value Objects (e.g., when a field must be true but is false).
/// It carries the specific validation failures from FluentValidation.
/// </para>
/// <para>
/// It provides specialized constructors for internal use, ensuring a consistent error code
/// and self-identifying aggregate type for this constraint violation.
/// </para>
/// </remarks>
public class BoolValueObjectValidationException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BoolValueObjectValidationException"/> class with full context information.
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate that caused the exception.</param>
    /// <param name="aggregateType">The type identifier of the aggregate (e.g., entity name).</param>
    /// <param name="aggregateCode">A specific code identifying the exception type (often a unique GUID).</param>
    /// <param name="aggregateHttpCode">The recommended HTTP status code for this exception type.</param>
    /// <param name="aggregateOccurredAt">The UTC date and time the exception occurred.</param>
    /// <param name="message">A user-friendly description of the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    /// <param name="validations">A list of specific validation failures associated with the constraint violation.</param>
    public BoolValueObjectValidationException(
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
    /// Forwarding constructor with nullable metadata; used by derived value-object exceptions to set their own code and type.
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate that caused the exception, or null to auto-generate one.</param>
    /// <param name="aggregateType">The type identifier of the aggregate, or null to use the default.</param>
    /// <param name="aggregateCode">A specific code identifying the exception type, or null to use the default.</param>
    /// <param name="aggregateHttpCode">The recommended HTTP status code for this exception type, or null to use the default.</param>
    /// <param name="aggregateOccurredAt">The UTC date and time the exception occurred, or null to use the current time.</param>
    /// <param name="message">A user-friendly description of the error, or null to build one from the validations.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    /// <param name="validations">The validation failures that triggered this exception.</param>
    public BoolValueObjectValidationException(
        Guid? aggregateId,
        string? aggregateType,
        Guid? aggregateCode,
        int? aggregateHttpCode,
        DateTime? aggregateOccurredAt,
        string? message,
        System.Exception? innerException,
        IEnumerable<ValidationFailure> validations
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
    /// Initializes a new instance of the <see cref="BoolValueObjectValidationException"/> class from the given validation failures, applying the default error code for this type.
    /// </summary>
    /// <param name="validations">The validation failures that triggered this exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    public BoolValueObjectValidationException(
        IEnumerable<ValidationFailure> validations,
        System.Exception? innerException = null
    ) : base(
        null,
        typeof(BoolValueObjectValidationException).FullName ?? typeof(BoolValueObjectValidationException).Name,
#if NET9_0_OR_GREATER
    new Guid("01951db1-9cf8-72ba-9bea-398222a41bec")
#else
    new Guid("22045d32-bade-4937-8bd5-63373e8c8738")
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
