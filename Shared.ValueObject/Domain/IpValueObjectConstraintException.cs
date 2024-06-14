using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class IpValueObjectConstraintException : StringValueObjectConstraintException
{
    public IpValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner, constraints) { }
    public IpValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, inner, constraints) { }
    public IpValueObjectConstraintException(System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(new Guid("e0a9bb6f-6e32-47f4-97fa-7b3ae3df67b7"), "IpValueObject Constraint Exception", inner, constraints) { }
}