using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class OrderByValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : StringValueObjectConstraintException(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
{
    public OrderByValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : this(constraints, Guid.NewGuid(), aggregateTypeId, 400, DateTime.UtcNow, message, inner) { }
    public OrderByValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : this(constraints, new Guid("cb76bd2d-e350-476d-bc72-c1a70d47451d"), "OrderByValueObject Constraint Exception", inner) { }
}