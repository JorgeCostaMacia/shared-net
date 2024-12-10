using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class OrderByValueObjectConstraintException : StringValueObjectConstraintException
{
    public OrderByValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public OrderByValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, innerException, constraints) { }
    public OrderByValueObjectConstraintException(System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(new Guid("cb76bd2d-e350-476d-bc72-c1a70d47451d"), $"{typeof(OrderByValueObject).Name} Constraint Exception", innerException, constraints) { }
}