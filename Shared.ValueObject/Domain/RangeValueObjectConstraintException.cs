using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain
{
    public class RangeValueObjectConstraintException : IConstraintException
    {
        public RangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public RangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public RangeValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("09e6cc67-11dd-4b0f-8385-beb18b6a4713"), "RangeValueObject Constraint Exception", inner)
        {
        }
    }
}