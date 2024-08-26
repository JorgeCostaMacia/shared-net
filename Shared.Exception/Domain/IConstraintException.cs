using FluentValidation.Results;
using System.Collections.Immutable;

namespace Shared.Exception.Domain;

public abstract class IConstraintException : IAggregateException
{
    public ImmutableList<ValidationFailure> Constraints { get; init; }

    protected IConstraintException(Guid id, Guid typeId, int code, DateTime occurredAt, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(id, typeId, code, occurredAt, message, inner)
    {
        Constraints = constraints.ToImmutableList();
    }

    protected IConstraintException(Guid typeId, string message, System.Exception? inner, IEnumerable<ValidationFailure> constraints) : base(Guid.NewGuid(), typeId, 400, $"{message} => {string.Join(",", constraints.Select(e => e.PropertyName + ": " + e.ErrorMessage))}", inner)
    {
        Constraints = constraints.ToImmutableList();
    }
}