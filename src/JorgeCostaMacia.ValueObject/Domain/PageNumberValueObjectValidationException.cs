using System.Collections.Immutable;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents a domain exception thrown when one or more constraints fail validation
/// on a <c>PageNumberValueObject</c>.
/// </summary>
/// <remarks>
/// <para>
/// This exception inherits from <see cref="IntValueObjectValidationException"/> and is used to signal
/// a violation of the domain's business rules specific to pagination (e.g., page number is zero or negative).
/// It carries the specific validation failures from FluentValidation.
/// </para>
/// <para>
/// It provides specialized constructors for internal use, ensuring a consistent error code
/// and self-identifying aggregate type for this specific constraint violation.
/// </para>
/// </remarks>
public class PageNumberValueObjectValidationException : IntValueObjectValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PageNumberValueObjectValidationException"/> class with full context information.
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate that caused the exception.</param>
    /// <param name="aggregateType">The type identifier of the aggregate (e.g., entity name).</param>
    /// <param name="aggregateCode">A specific code identifying the exception type (often a unique GUID).</param>
    /// <param name="aggregateHttpCode">The recommended HTTP status code for this exception type.</param>
    /// <param name="aggregateOccurredAt">The UTC date and time the exception occurred.</param>
    /// <param name="message">A user-friendly description of the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    /// <param name="validations">A list of specific validation failures associated with the constraint violation.</param>
    public PageNumberValueObjectValidationException(
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
    public PageNumberValueObjectValidationException(
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
    /// Initializes a new instance of the <see cref="PageNumberValueObjectValidationException"/> class from the given validation failures, applying the default error code for this type.
    /// </summary>
    /// <param name="validations">The validation failures that triggered this exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    public PageNumberValueObjectValidationException(
        IEnumerable<ValidationFailure> validations,
        System.Exception? innerException = null
    ) : base(
        null,
        typeof(PageNumberValueObjectValidationException).FullName ?? typeof(PageNumberValueObjectValidationException).Name,
#if NET9_0_OR_GREATER
    new Guid("01951f43-542a-7cd3-9751-f30eb290dd91")
#else
    new Guid("113b86ca-58af-4d7d-8a22-90de8c399ac3")
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
