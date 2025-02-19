using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class BoolValueObjectConstraintException : IConstraintException
{
    public BoolValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public BoolValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951db1-9cf8-72ba-9bea-398222a41bec"), aggregateCode, aggregateOccurredAt, $"{typeof(BoolValueObject).Name} Constraint Exception", innerException, constraints) { }
}