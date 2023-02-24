namespace Shared.Exception.Domain
{
    public abstract class INotUpdateException : IAggregateException
    {
        public INotUpdateException() : base(new Guid("0d082e22-462c-47ac-b989-d511f84a1ff7"), 409, "Not Update Exception")
        {
        }

        public INotUpdateException(string message) : base(new Guid("0d082e22-462c-47ac-b989-d511f84a1ff7"), 409, message)
        {
        }

        public INotUpdateException(string message, System.Exception inner) : base(new Guid("0d082e22-462c-47ac-b989-d511f84a1ff7"), 409, message, inner)
        {
        }

        public INotUpdateException(Guid aggregateId, string message) : base(aggregateId, 409, message)
        {
        }

        public INotUpdateException(Guid aggregateId, string message, System.Exception inner) : base(aggregateId, 409, message, inner)
        {
        }
    }
}
