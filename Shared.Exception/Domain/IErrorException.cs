using System.Collections.Immutable;

namespace Shared.Exception.Domain
{
    public class IErrorException : IAggregateException
    {
        public IImmutableList<string> Errors { get; protected set; }

        protected IErrorException(IEnumerable<string> errors, Guid aggregateId, Guid aggregateTypeId, int aggregateCode, DateTime aggregateOccurredAt, string message, System.Exception? inner) : base(aggregateId, aggregateTypeId, aggregateCode, aggregateOccurredAt, message, inner)
        {
            Errors = errors.ToImmutableList();
        }

        protected IErrorException(IEnumerable<string> errors, Guid aggregateTypeId, string message, System.Exception? inner) : base(Guid.NewGuid(), aggregateTypeId, 500, DateTime.UtcNow, $"{message} => {string.Join(",", errors)}", inner)
        {
            Errors = errors.ToImmutableList();
        }
    }
}
