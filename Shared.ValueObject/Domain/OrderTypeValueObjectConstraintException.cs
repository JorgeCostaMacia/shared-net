using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class OrderTypeValueObjectConstraintException : StringValueObjectConstraintException
{
    public OrderTypeValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public OrderTypeValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f42-bb30-7ebe-8bf5-c6ee31b6789a"), aggregateCode, aggregateOccurredAt, $"{typeof(OrderTypeValueObject).Name} Constraint Exception", innerException, constraints) { }
}