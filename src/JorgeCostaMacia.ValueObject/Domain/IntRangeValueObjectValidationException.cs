using FluentValidation.Results;
using JorgeCostaMacia.Exception.Domain;
using System.Collections.Immutable;

namespace JorgeCostaMacia.ValueObject.Domain;

/// <summary>
/// Represents a domain exception thrown when one or more constraints fail validation
/// on an <c>IntRangeValueObject</c> or any derived Value Object.
/// </summary>
/// <remarks>
/// <para>
/// This exception inherits from <see cref="ValidationException"/> and is used to signal
/// a violation of the domain's business rules related to integer range Value Objects (e.g., start value is greater than end value).
/// It carries the specific validation failures from FluentValidation.
/// </para>
/// <para>
/// It provides specialized constructors for internal use, ensuring a consistent error code
/// and self-identifying aggregate type for this constraint violation.
/// </para>
/// </remarks>
public class IntRangeValueObjectValidationException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntRangeValueObjectValidationException"/> class with full context information.
    /// </summary>
    /// <param name="aggregateId">The unique identifier of the aggregate that caused the exception.</param>
    /// <param name="aggregateType">The type identifier of the aggregate (e.g., entity name).</param>
    /// <param name="aggregateCode">A specific code identifying the exception type (often a unique GUID).</param>
    /// <param name="aggregateHttpCode">The recommended HTTP status code for this exception type.</param>
    /// <param name="aggregateOccurredAt">The UTC date and time the exception occurred.</param>
    /// <param name="message">A user-friendly description of the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    /// <param name="validations">A list of specific validation failures associated with the constraint violation.</param>
    public IntRangeValueObjectValidationException(
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
    /// Initializes a new instance of the <see cref="IntRangeValueObjectValidationException"/> class
    /// using only the necessary constraint information.
    /// </summary>
    /// <remarks>
    /// This constructor automatically sets the following default values:
    /// <list type="bullet">
    ///     <item><description>Code: <c>01951f40-ed09-7091-b79d-f0c7276d613c</c> (Unique identifier for this exception).</description></item>
    ///     <item><description>Type: <c>IntRangeValueObjectValidationException</c>.</description></item>
    ///     <item><description>HTTP Code, Message, ID, and Occurred At: <c>null</c> or default values.</description></item>
    /// </list>
    /// </remarks>
    /// <param name="aggregateId">The unique identifier of the aggregate that caused the exception, or null.</param>
    /// <param name="aggregateType">The type identifier of the aggregate (e.g., entity name).</param>
    /// <param name="aggregateCode">A specific code identifying the exception type (often a unique GUID).</param>
    /// <param name="aggregateHttpCode">The recommended HTTP status code for this exception type, or null.</param>
    /// <param name="aggregateOccurredAt">The UTC date and time the exception occurred, or null.</param>
    /// <param name="message">A user-friendly description of the error, or null.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or null.</param>
    /// <param name="validations">A list of specific validation failures associated with the constraint violation.</param>
    public IntRangeValueObjectValidationException(
        IEnumerable<ValidationFailure> validations,
        System.Exception? innerException = null
    ) : base(
        null,
        typeof(IntRangeValueObjectValidationException).FullName ?? typeof(IntRangeValueObjectValidationException).Name,
#if NET9_0_OR_GREATER
    new Guid("01951f40-ed09-7091-b79d-f0c7276d613c")
#else
    new Guid("21f7b715-6c5e-4cba-a8c0-de7629f69463")
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