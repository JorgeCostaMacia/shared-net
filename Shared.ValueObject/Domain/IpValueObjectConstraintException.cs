using FluentValidation.Results;

namespace Shared.ValueObject.Domain
{
    public class IpValueObjectConstraintException : StringValueObjectConstraintException
    {
        public IpValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(constraints, aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
        }

        public IpValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, Guid aggregateTypeId, string message, System.Exception? inner) : base(constraints, aggregateTypeId, message, inner)
        {
        }

        public IpValueObjectConstraintException(IEnumerable<ValidationFailure> constraints, System.Exception? inner = null) : base(constraints, new Guid("e0a9bb6f-6e32-47f4-97fa-7b3ae3df67b7"), "IpValueObject Constraint Exception", inner)
        {
        }
    }
}
