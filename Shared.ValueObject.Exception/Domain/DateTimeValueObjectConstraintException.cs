using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class DateTimeValueObjectConstraintException : IConstraintException<DateTimeValueObject>
    {
        public DateTimeValueObjectConstraintException(string property, DateTimeValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public DateTimeValueObjectConstraintException(string property, DateTimeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public DateTimeValueObjectConstraintException(DateTimeValueObject value, List<string> constraints) : base("DateTimeValueObject", value, constraints, new Guid("88cd6dcb-9795-48c4-b27d-15f9c554a433"), "DateTimeValueObject Constraint Exception")
        {
        }

        public DateTimeValueObjectConstraintException(DateTimeValueObject value, List<string> constraints, System.Exception inner) : base("DateTimeValueObject", value, constraints, new Guid("88cd6dcb-9795-48c4-b27d-15f9c554a433"), "DateTimeValueObject Constraint Exception", inner)
        {
        }

        public DateTimeValueObjectConstraintException(DateTimeValueObject value, List<string> constraints, string message) : base("DateTimeValueObject", value, constraints, new Guid("88cd6dcb-9795-48c4-b27d-15f9c554a433"), message)
        {
        }

        public DateTimeValueObjectConstraintException(DateTimeValueObject value, List<string> constraints, string message, System.Exception inner) : base("DateTimeValueObject", value, constraints, new Guid("88cd6dcb-9795-48c4-b27d-15f9c554a433"), message, inner)
        {
        }

        public DateTimeValueObjectConstraintException(DateTimeValueObject value, List<string> constraints, Guid id, string message) : base("DateTimeValueObject", value, constraints, id, message)
        {
        }

        public DateTimeValueObjectConstraintException(DateTimeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("DateTimeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
