using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class GroupTypeValueObjectConstraintException : IConstraintException<GroupTypeValueObject>
    {
        public GroupTypeValueObjectConstraintException(string property, GroupTypeValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public GroupTypeValueObjectConstraintException(string property, GroupTypeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public GroupTypeValueObjectConstraintException(GroupTypeValueObject value, List<string> constraints) : base("GroupTypeValueObject", value, constraints, new Guid("0987410e-b997-44b0-aee4-34d579723201"), "GroupTypeValueObject Constraint Exception")
        {
        }

        public GroupTypeValueObjectConstraintException(GroupTypeValueObject value, List<string> constraints, System.Exception inner) : base("GroupTypeValueObject", value, constraints, new Guid("0987410e-b997-44b0-aee4-34d579723201"), "GroupTypeValueObject Constraint Exception", inner)
        {
        }

        public GroupTypeValueObjectConstraintException(GroupTypeValueObject value, List<string> constraints, string message) : base("GroupTypeValueObject", value, constraints, new Guid("0987410e-b997-44b0-aee4-34d579723201"), message)
        {
        }

        public GroupTypeValueObjectConstraintException(GroupTypeValueObject value, List<string> constraints, string message, System.Exception inner) : base("GroupTypeValueObject", value, constraints, new Guid("0987410e-b997-44b0-aee4-34d579723201"), message, inner)
        {
        }

        public GroupTypeValueObjectConstraintException(GroupTypeValueObject value, List<string> constraints, Guid id, string message) : base("GroupTypeValueObject", value, constraints, id, message)
        {
        }

        public GroupTypeValueObjectConstraintException(GroupTypeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("GroupTypeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
