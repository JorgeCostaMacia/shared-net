using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class OrderTypeValueObjectConstraintException : StringValueObjectConstraintException
{
    public OrderTypeValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public OrderTypeValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, innerException, constraints) { }
    public OrderTypeValueObjectConstraintException(System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(new Guid("ca7099f9-0b56-4076-b24f-89f9bf173608"), "OrderTypeValueObject Constraint Exception", innerException, constraints) { }
}