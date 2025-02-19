using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class DateTimeRangeValueObjectConstraintException : IConstraintException
{
    public DateTimeRangeValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public DateTimeRangeValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f26-fd3e-7370-be17-d590fb9e54b9"), aggregateCode, aggregateOccurredAt, $"{typeof(DateTimeRangeValueObject).Name} Constraint Exception", innerException, constraints) { }
}