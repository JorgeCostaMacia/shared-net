using System.Collections.Immutable;
using FluentValidation.Results;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents a domain exception thrown when one or more constraints fail validation
/// on an <c>OrderByValueObject</c>.
/// </summary>
/// <remarks>
/// <para>
/// This exception inherits from <see cref="StringValueObjectValidationException"/> and is used to signal
/// a violation of the domain's business rules specific to sorting criteria (e.g., invalid field name, unsupported sort direction).
/// It carries the specific validation failures from FluentValidation.
/// </para>
/// <para>
/// It provides specialized constructors for internal use, ensuring a consistent error code
/// and self-identifying aggregate type for this specific constraint violation.
/// </para>
/// </remarks>
public class OrderByValueObjectValidationException : StringValueObjectValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderByValueObjectValidationException"/> class with full context information.
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate that caused the exception.</param>
    /// <param name="aggregateType">The type identifier of the aggregate (e.g., entity name).</param>
    /// <param name="aggregateCode">A specific code identifying the exception type (often a unique GUID).</param>
    /// <param name="aggregateHttpCode">The recommended HTTP status code for this exception type.</param>
    /// <param name="aggregateOccurredAt">The UTC date and time the exception occurred.</param>
    /// <param name="message">A user-friendly description of the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    /// <param name="validations">A list of specific validation failures associated with the constraint violation.</param>
    public OrderByValueObjectValidationException(
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
    /// Initializes a new instance of the <see cref="OrderByValueObjectValidationException"/> class from the given validation failures, applying the default error code for this type.
    /// </summary>
    /// <param name="validations">The validation failures that triggered this exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    public OrderByValueObjectValidationException(
        IEnumerable<ValidationFailure> validations,
        System.Exception? innerException = null
    ) : base(
        null,
        typeof(OrderByValueObjectValidationException).FullName ?? typeof(OrderByValueObjectValidationException).Name,
#if NET9_0_OR_GREATER
    new Guid("01951f42-7f59-7e43-8484-686d8de64b80")
#else
    new Guid("87b0b944-7d1a-4b9c-bda7-f66433658acb")
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