using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class OrderTypeValueObjectConstraintException : IConstraintException<OrderTypeValueObject>
    {
        public OrderTypeValueObjectConstraintException(string property, OrderTypeValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public OrderTypeValueObjectConstraintException(string property, OrderTypeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public OrderTypeValueObjectConstraintException(OrderTypeValueObject value, List<string> constraints) : base("OrderTypeValueObject", value, constraints, new Guid("ca7099f9-0b56-4076-b24f-89f9bf173608"), "OrderTypeValueObject Constraint Exception")
        {
        }

        public OrderTypeValueObjectConstraintException(OrderTypeValueObject value, List<string> constraints, System.Exception inner) : base("OrderTypeValueObject", value, constraints, new Guid("ca7099f9-0b56-4076-b24f-89f9bf173608"), "OrderTypeValueObject Constraint Exception", inner)
        {
        }

        public OrderTypeValueObjectConstraintException(OrderTypeValueObject value, List<string> constraints, string message) : base("OrderTypeValueObject", value, constraints, new Guid("ca7099f9-0b56-4076-b24f-89f9bf173608"), message)
        {
        }

        public OrderTypeValueObjectConstraintException(OrderTypeValueObject value, List<string> constraints, string message, System.Exception inner) : base("OrderTypeValueObject", value, constraints, new Guid("ca7099f9-0b56-4076-b24f-89f9bf173608"), message, inner)
        {
        }

        public OrderTypeValueObjectConstraintException(OrderTypeValueObject value, List<string> constraints, Guid id, string message) : base("OrderTypeValueObject", value, constraints, id, message)
        {
        }

        public OrderTypeValueObjectConstraintException(OrderTypeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("OrderTypeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
