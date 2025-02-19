using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class EmailValueObjectConstraintException : StringValueObjectConstraintException
{
    public EmailValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public EmailValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f3f-69d9-73e3-be78-61a2210d3e62"), aggregateCode, aggregateOccurredAt, $"{typeof(EmailValueObject).Name} Constraint Exception", innerException, constraints) { }
}