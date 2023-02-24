namespace Shared.Exception.Domain
{
    public abstract class INotFoundException : IAggregateException
    {
        public INotFoundException() : base(new Guid("42fd7e5d-0520-4065-9fc8-850731924734"), 404, "Not Fount Exception")
        {
        }

        public INotFoundException(string message) : base(new Guid("42fd7e5d-0520-4065-9fc8-850731924734"), 404, message)
        {
        }

        public INotFoundException(string message, System.Exception inner) : base(new Guid("42fd7e5d-0520-4065-9fc8-850731924734"), 404, message, inner)
        {
        }

        public INotFoundException(Guid aggregateId, string message) : base(aggregateId, 404, message)
        {
        }

        public INotFoundException(Guid aggregateId, string message, System.Exception inner) : base(aggregateId, 404, message, inner)
        {
        }
    }
}
