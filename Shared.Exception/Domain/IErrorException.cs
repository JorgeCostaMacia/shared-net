using System.Collections.Immutable;

namespace Shared.Exception.Domain;

public class IErrorException : IException
{
    public ImmutableList<string> Errors { get; init; }

    protected IErrorException(Guid id, Guid type, int code, DateTime occurredAt, string message, System.Exception? inner, IEnumerable<string> errors) : base(id, type, code, occurredAt, message, inner)
    {
        Errors = errors.ToImmutableList();
    }

    protected IErrorException(Guid type, string message, System.Exception? inner, IEnumerable<string> errors) : base(Guid.NewGuid(), type, 500, $"{message} => {string.Join(",", errors)}", inner)
    {
        Errors = errors.ToImmutableList();
    }
}
