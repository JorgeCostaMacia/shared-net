using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain
{
    public class DateTimeRangeValueObjectConstraintException : IConstraintException
    {
        public DateTimeRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public DateTimeRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public DateTimeRangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("cbb92bf8-887e-4adc-a255-bf87aacd73ce"), "DateTimeRangeValueObject Constraint Exception", inner)
        {
        }
    }
}
