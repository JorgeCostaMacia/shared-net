using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class DateTimeRangeValueObjectConstraintException : IConstraintException
{
    public DateTimeRangeValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner, constraints) { }
    public DateTimeRangeValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, inner, constraints) { }
    public DateTimeRangeValueObjectConstraintException(System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(new Guid("cbb92bf8-887e-4adc-a255-bf87aacd73ce"), "DateTimeRangeValueObject Constraint Exception", inner, constraints) { }
}