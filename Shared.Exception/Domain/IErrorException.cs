using System.Collections.Immutable;

namespace Shared.Exception.Domain;

public abstract class IErrorException : IAggregateException
{
    public ImmutableList<string> Errors { get; init; }

    protected IErrorException(Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? innerException, IEnumerable<string> errors) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, innerException)
    {
        Errors = errors.ToImmutableList();
    }


    protected IErrorException(Guid aggregateTypeId, string message, System.Exception? innerException, IEnumerable<string> errors) : base(
#if NET9_0
        Guid.CreateVersion7()
#else
        Guid.NewGuid()
#endif
        , aggregateTypeId, 500, $"{message} => {string.Join(",", errors)}", innerException)
    {
        Errors = errors.ToImmutableList();
    }
}