using FluentValidation.Results;
using System.Collections.Immutable;

namespace Shared.Exception.Domain;

public abstract class IConstraintException : IAggregateException
{
    public ImmutableList<ValidationFailure> Constraints { get; init; }

    protected IConstraintException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException)
    {
        Constraints = constraints.ToImmutableList();
    }
    protected IConstraintException(Guid aggregateTypeId, string message, System.Exception? innerException, IEnumerable<ValidationFailure> constraints) : base(
#if NET9_0
        Guid.CreateVersion7()
#else
        Guid.NewGuid()
#endif
        , aggregateTypeId, 400, $"{message} => {string.Join(",", constraints.Select(e => e.PropertyName + ": " + e.ErrorMessage))}", innerException)
    {
        Constraints = constraints.ToImmutableList();
    }
}