using System.Collections.Immutable;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents a domain exception thrown when one or more constraints fail validation
/// on a <c>JsonValueObject</c>.
/// </summary>
/// <remarks>
/// <para>
/// This exception inherits from <see cref="StringValueObjectValidationException"/> and is used to signal
/// a violation of the domain's business rules specific to JSON string validation (e.g., malformed JSON syntax).
/// It carries the specific validation failures from FluentValidation.
/// </para>
/// <para>
/// It provides specialized constructors for internal use, ensuring a consistent error code
/// and self-identifying aggregate type for this specific constraint violation.
/// </para>
/// </remarks>
public class JsonValueObjectValidationException : StringValueObjectValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonValueObjectValidationException"/> class with full context information.
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate that caused the exception.</param>
    /// <param name="aggregateType">The type identifier of the aggregate (e.g., entity name).</param>
    /// <param name="aggregateCode">A specific code identifying the exception type (often a unique GUID).</param>
    /// <param name="aggregateHttpCode">The recommended HTTP status code for this exception type.</param>
    /// <param name="aggregateOccurredAt">The UTC date and time the exception occurred.</param>
    /// <param name="message">A user-friendly description of the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    /// <param name="validations">A list of specific validation failures associated with the constraint violation.</param>
    public JsonValueObjectValidationException(
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
    /// Initializes a new instance of the <see cref="JsonValueObjectValidationException"/> class from the given validation failures, applying the default error code for this type.
    /// </summary>
    /// <param name="validations">The validation failures that triggered this exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    public JsonValueObjectValidationException(
        IEnumerable<ValidationFailure> validations,
        System.Exception? innerException = null
    ) : base(
        null,
        typeof(JsonValueObjectValidationException).FullName ?? typeof(JsonValueObjectValidationException).Name,
#if NET9_0_OR_GREATER
    new Guid("01951f42-0686-7b9b-bb04-c1cd39ecdf75")
#else
    new Guid("f0db9008-f7a9-4280-9f54-8fe4efef1398")
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
