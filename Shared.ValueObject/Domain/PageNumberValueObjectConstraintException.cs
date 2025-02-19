using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageNumberValueObjectConstraintException : IntValueObjectConstraintException
{
    public PageNumberValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public PageNumberValueObjectConstraintException(Guid? aggregateId, Guid? aggregateTypeId, int? aggregateCode, DateTime? aggregateOccurredAt, string? message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId ?? new Guid("01951f43-542a-7cd3-9751-f30eb290dd91"), aggregateCode, aggregateOccurredAt, $"{typeof(PageNumberValueObject).Name} Constraint Exception", innerException, constraints) { }
}