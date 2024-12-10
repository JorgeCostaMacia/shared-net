using FluentValidation.Results;

namespace Shared.ValueObject.Domain;

public class PageNumberValueObjectConstraintException : IntValueObjectConstraintException
{
    public PageNumberValueObjectConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException, constraints) { }
    public PageNumberValueObjectConstraintException(Guid aggregateTypeId, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateTypeId, message, innerException, constraints) { }
    public PageNumberValueObjectConstraintException(System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(new Guid("df74e0e0-e20c-4135-b583-690f15b8ac1d"), $"{typeof(PageNumberValueObject).Name} Constraint Exception", innerException, constraints) { }
}