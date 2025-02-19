using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class UrlValueObjectConstraintException : StringValueObjectConstraintException
{
    public UrlValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public UrlValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f44-0397-7ea0-8872-ef9b2ba6bfb1"), aggregateCode, aggregateOccurredAt, $"{typeof(UrlValueObject).Name} Constraint Exception", innerException, constraints) { }
}