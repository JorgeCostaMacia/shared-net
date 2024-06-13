using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class FloatValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : IConstraintException(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
{
    public FloatValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : this(constraints, Guid.NewGuid(), aggregateTypeId, 400, DateTime.UtcNow, message, inner) { }
    public FloatValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : this(constraints, new Guid("18bc336e-3322-40c9-a19d-e041238434b1"), "FloatValueObject Constraint Exception", inner) { }
}