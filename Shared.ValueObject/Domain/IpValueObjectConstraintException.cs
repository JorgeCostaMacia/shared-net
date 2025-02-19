using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class IpValueObjectConstraintException : StringValueObjectConstraintException
{
    public IpValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public IpValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f41-9ac1-7804-9d2d-adfe0eeb472b"), aggregateCode, aggregateOccurredAt, $"{typeof(IpValueObject).Name} Constraint Exception", innerException, constraints) { }
}