using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageNumberRangeValueObjectConstraintException : IntRangeValueObjectConstraintException
{
    public PageNumberRangeValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public PageNumberRangeValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f43-12aa-7b22-97bf-c1b05d5f8dd7"), aggregateCode, aggregateOccurredAt, $"{typeof(PageNumberRangeValueObject).Name} Constraint Exception", innerException, constraints) { }
}