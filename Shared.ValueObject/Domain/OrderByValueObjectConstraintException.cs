using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class OrderByValueObjectConstraintException : StringValueObjectConstraintException
{
    public OrderByValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public OrderByValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f42-7f59-7e43-8484-686d8de64b80"), aggregateCode, aggregateOccurredAt, $"{typeof(OrderByValueObject).Name} Constraint Exception", innerException, constraints) { }
}