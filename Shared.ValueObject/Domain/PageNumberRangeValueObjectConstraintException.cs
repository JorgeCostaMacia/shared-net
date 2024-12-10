using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageNumberRangeValueObjectConstraintException : IntRangeValueObjectConstraintException
{
    public PageNumberRangeValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public PageNumberRangeValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, innerException, constraints) { }
    public PageNumberRangeValueObjectConstraintException(System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(new Guid("c5392e0b-afd2-4bfb-8447-1d5693ffce51"), $"{typeof(PageNumberRangeValueObject).Name} Constraint Exception", innerException, constraints) { }
}