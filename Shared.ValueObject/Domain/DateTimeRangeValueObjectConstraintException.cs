using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class DateTimeRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : IConstraintException(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
{
    public DateTimeRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : this(constraints, Guid.NewGuid(), aggregateTypeId, 400, DateTime.UtcNow, message, inner) { }
    public DateTimeRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : this(constraints, new Guid("cbb92bf8-887e-4adc-a255-bf87aacd73ce"), "DateTimeRangeValueObject Constraint Exception", inner) { }
}