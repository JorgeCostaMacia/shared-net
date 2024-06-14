using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class GroupByValueObjectConstraintException : StringValueObjectConstraintException
{
    public GroupByValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner, constraints) { }
    public GroupByValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, inner, constraints) { }
    public GroupByValueObjectConstraintException(System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(new Guid("1e2ffc5a-a2fa-4be2-8837-4fefe0520ef5"), "GroupByValueObject Constraint Exception", inner, constraints) { }
}