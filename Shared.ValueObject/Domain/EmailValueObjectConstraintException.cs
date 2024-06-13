using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class EmailValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : StringValueObjectConstraintException(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
{
    public EmailValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : this(constraints, Guid.NewGuid(), aggregateTypeId, 400, DateTime.UtcNow, message, inner) { }
    public EmailValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : this(constraints, new Guid("9ab52afe-120e-47b4-a502-b41b5474f910"), "EmailValueObject Constraint Exception", inner) { }
}