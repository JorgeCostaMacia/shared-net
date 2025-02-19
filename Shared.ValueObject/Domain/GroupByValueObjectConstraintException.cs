using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class GroupByValueObjectConstraintException : StringValueObjectConstraintException
{
    public GroupByValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public GroupByValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f40-7d68-762c-9214-6ffc5730839b"), aggregateCode, aggregateOccurredAt, $"{typeof(GroupByValueObject).Name} Constraint Exception", innerException, constraints) { }
}