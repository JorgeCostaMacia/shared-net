using Shared.Exception.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class GroupByValueObjectConstraintException : IConstraintException<string>
    {
        public GroupByValueObjectConstraintException(string value, List<string> constraints) : base("GroupByValueObject", value, constraints, new Guid("1e2ffc5a-a2fa-4be2-8837-4fefe0520ef5"), "GroupByValueObject Constraint Exception")
        {
        }

        public GroupByValueObjectConstraintException(string value, List<string> constraints, System.Exception inner) : base("GroupByValueObject", value, constraints, new Guid("1e2ffc5a-a2fa-4be2-8837-4fefe0520ef5"), "GroupByValueObject Constraint Exception", inner)
        {
        }

        public GroupByValueObjectConstraintException(string value, List<string> constraints, string message) : base("GroupByValueObject", value, constraints, new Guid("1e2ffc5a-a2fa-4be2-8837-4fefe0520ef5"), message)
        {
        }

        public GroupByValueObjectConstraintException(string value, List<string> constraints, string message, System.Exception inner) : base("GroupByValueObject", value, constraints, new Guid("1e2ffc5a-a2fa-4be2-8837-4fefe0520ef5"), message, inner)
        {
        }

        public GroupByValueObjectConstraintException(string value, List<string> constraints, Guid id, string message) : base("GroupByValueObject", value, constraints, id, message)
        {
        }

        public GroupByValueObjectConstraintException(string value, List<string> constraints, Guid id, string message, System.Exception inner) : base("GroupByValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
