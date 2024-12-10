using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class IpValueObjectConstraintException : StringValueObjectConstraintException
{
    public IpValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public IpValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, innerException, constraints) { }
    public IpValueObjectConstraintException(System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(new Guid("e0a9bb6f-6e32-47f4-97fa-7b3ae3df67b7"), $"{typeof(IpValueObject).Name} Constraint Exception", innerException, constraints) { }
}