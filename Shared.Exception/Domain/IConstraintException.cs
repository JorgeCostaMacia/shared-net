namespace Shared.Exception.Domain
{
    public abstract class IConstraintException<T> : IAggregateException
    {
        public string Property { get; }
        public T Value { get; }
        public List<string> Constraints { get; }

        public IConstraintException(string property, T value, List<string> constraints) : base(new Guid("5ab71773-dbd2-47e6-8cdd-49d0ea5658dc"), 400, "Constraint Exception")
        {
            Property = property;
            Value = value;
            Constraints = constraints;
        }

        public IConstraintException(string property, T value, List<string> constraints, string message) : base(new Guid("5ab71773-dbd2-47e6-8cdd-49d0ea5658dc"), 400, message)
        {
            Property = property;
            Value = value;
            Constraints = constraints;
        }

        public IConstraintException(string property, T value, List<string> constraints, string message, System.Exception inner) : base(new Guid("5ab71773-dbd2-47e6-8cdd-49d0ea5658dc"), 400, message, inner)
        {
            Property = property;
            Value = value;
            Constraints = constraints;
        }

        public IConstraintException(string property, T value, List<string> constraints, Guid aggregateId, string message) : base(aggregateId, 400, message)
        {
            Property = property;
            Value = value;
            Constraints = constraints;
        }

        public IConstraintException(string property, T value, List<string> constraints, Guid aggregateId, string message, System.Exception inner) : base(aggregateId, 400, message, inner)
        {
            Property = property;
            Value = value;
            Constraints = constraints;
        }
    }
}
