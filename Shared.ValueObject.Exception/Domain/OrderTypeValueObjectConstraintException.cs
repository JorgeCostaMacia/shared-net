using Shared.Exception.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class OrderTypeValueObjectConstraintException : IConstraintException<int>
    {
        public OrderTypeValueObjectConstraintException(string property, int value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public OrderTypeValueObjectConstraintException(string property, int value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public OrderTypeValueObjectConstraintException(int value, List<string> constraints) : base("OrderTypeValueObject", value, constraints, new Guid("ca7099f9-0b56-4076-b24f-89f9bf173608"), "OrderTypeValueObject Constraint Exception")
        {
        }

        public OrderTypeValueObjectConstraintException(int value, List<string> constraints, System.Exception inner) : base("OrderTypeValueObject", value, constraints, new Guid("ca7099f9-0b56-4076-b24f-89f9bf173608"), "OrderTypeValueObject Constraint Exception", inner)
        {
        }

        public OrderTypeValueObjectConstraintException(int value, List<string> constraints, string message) : base("OrderTypeValueObject", value, constraints, new Guid("ca7099f9-0b56-4076-b24f-89f9bf173608"), message)
        {
        }

        public OrderTypeValueObjectConstraintException(int value, List<string> constraints, string message, System.Exception inner) : base("OrderTypeValueObject", value, constraints, new Guid("ca7099f9-0b56-4076-b24f-89f9bf173608"), message, inner)
        {
        }

        public OrderTypeValueObjectConstraintException(int value, List<string> constraints, Guid id, string message) : base("OrderTypeValueObject", value, constraints, id, message)
        {
        }

        public OrderTypeValueObjectConstraintException(int value, List<string> constraints, Guid id, string message, System.Exception inner) : base("OrderTypeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
