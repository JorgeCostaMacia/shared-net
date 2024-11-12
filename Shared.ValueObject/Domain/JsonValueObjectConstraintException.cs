using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class JsonValueObjectConstraintException : StringValueObjectConstraintException
{
    public JsonValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public JsonValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, innerException, constraints) { }
    public JsonValueObjectConstraintException(System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(new Guid("64c55eae-817a-4591-84cb-24dc2b33e9df"), "JsonValueObject Constraint Exception", innerException, constraints) { }
}