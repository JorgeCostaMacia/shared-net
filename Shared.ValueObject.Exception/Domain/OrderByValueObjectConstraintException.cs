using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class OrderByValueObjectConstraintException : IConstraintException<OrderByValueObject>
    {
        public OrderByValueObjectConstraintException(string property, OrderByValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public OrderByValueObjectConstraintException(string property, OrderByValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public OrderByValueObjectConstraintException(OrderByValueObject value, List<string> constraints) : base("OrderByValueObject", value, constraints, new Guid("cb76bd2d-e350-476d-bc72-c1a70d47451d"), "OrderByValueObject Constraint Exception")
        {
        }

        public OrderByValueObjectConstraintException(OrderByValueObject value, List<string> constraints, System.Exception inner) : base("OrderByValueObject", value, constraints, new Guid("cb76bd2d-e350-476d-bc72-c1a70d47451d"), "OrderByValueObject Constraint Exception", inner)
        {
        }

        public OrderByValueObjectConstraintException(OrderByValueObject value, List<string> constraints, string message) : base("OrderByValueObject", value, constraints, new Guid("cb76bd2d-e350-476d-bc72-c1a70d47451d"), message)
        {
        }

        public OrderByValueObjectConstraintException(OrderByValueObject value, List<string> constraints, string message, System.Exception inner) : base("OrderByValueObject", value, constraints, new Guid("cb76bd2d-e350-476d-bc72-c1a70d47451d"), message, inner)
        {
        }

        public OrderByValueObjectConstraintException(OrderByValueObject value, List<string> constraints, Guid id, string message) : base("OrderByValueObject", value, constraints, id, message)
        {
        }

        public OrderByValueObjectConstraintException(OrderByValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("OrderByValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
