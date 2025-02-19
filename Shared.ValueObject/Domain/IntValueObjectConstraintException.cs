using FluentValidation.Results;
using Shared.Exception.Domain;

namespace Shared.ValueObject.Domain;

public class IntValueObjectConstraintException : IConstraintException
{
    public IntValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public IntValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f41-4071-72b9-a614-5cb655b2bda9"), aggregateCode, aggregateOccurredAt, $"{typeof(IntValueObject).Name} Constraint Exception", innerException, constraints) { }
}