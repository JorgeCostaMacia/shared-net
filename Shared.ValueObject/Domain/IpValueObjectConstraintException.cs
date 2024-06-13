using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class IpValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : StringValueObjectConstraintException(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
{
    public IpValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : this(constraints, Guid.NewGuid(), aggregateTypeId, 400, DateTime.UtcNow, message, inner) { }
    public IpValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : this(constraints, new Guid("e0a9bb6f-6e32-47f4-97fa-7b3ae3df67b7"), "IpValueObject Constraint Exception", inner) { }
}
