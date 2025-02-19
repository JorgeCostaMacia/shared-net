using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageSizeValueObjectConstraintException : IntValueObjectConstraintException
{
    public PageSizeValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public PageSizeValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f43-cfdf-70ed-ac22-e0047d9318d0"), aggregateCode, aggregateOccurredAt, $"{typeof(PageSizeValueObject).Name} Constraint Exception", innerException, constraints) { }
}