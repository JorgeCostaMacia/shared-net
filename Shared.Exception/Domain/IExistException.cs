namespace Shared.Exception.Domain
{
    public abstract class IExistException : IAggregateException
    {
        public IExistException() : base(new Guid("c01cf409-1f93-40e2-8629-7779f5a84658"), 409, "Exist Exception")
        {
        }

        public IExistException(string message) : base(new Guid("c01cf409-1f93-40e2-8629-7779f5a84658"), 409, message)
        {
        }

        public IExistException(string message, System.Exception inner) : base(new Guid("c01cf409-1f93-40e2-8629-7779f5a84658"), 409, message, inner)
        {
        }

        public IExistException(Guid aggregateId, string message) : base(aggregateId, 409, message)
        {
        }

        public IExistException(Guid aggregateId, string message, System.Exception inner) : base(aggregateId, 409, message, inner)
        {
        }
    }
}
