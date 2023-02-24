using Shared.Exception.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class GroupTypeValueObjectConstraintException : IConstraintException<int>
    {
        public GroupTypeValueObjectConstraintException(int value, List<string> constraints) : base("GroupTypeValueObject", value, constraints, new Guid("0987410e-b997-44b0-aee4-34d579723201"), "GroupTypeValueObject Constraint Exception")
        {
        }

        public GroupTypeValueObjectConstraintException(int value, List<string> constraints, System.Exception inner) : base("GroupTypeValueObject", value, constraints, new Guid("0987410e-b997-44b0-aee4-34d579723201"), "GroupTypeValueObject Constraint Exception", inner)
        {
        }

        public GroupTypeValueObjectConstraintException(int value, List<string> constraints, string message) : base("GroupTypeValueObject", value, constraints, new Guid("0987410e-b997-44b0-aee4-34d579723201"), message)
        {
        }

        public GroupTypeValueObjectConstraintException(int value, List<string> constraints, string message, System.Exception inner) : base("GroupTypeValueObject", value, constraints, new Guid("0987410e-b997-44b0-aee4-34d579723201"), message, inner)
        {
        }

        public GroupTypeValueObjectConstraintException(int value, List<string> constraints, Guid id, string message) : base("GroupTypeValueObject", value, constraints, id, message)
        {
        }

        public GroupTypeValueObjectConstraintException(int value, List<string> constraints, Guid id, string message, System.Exception inner) : base("GroupTypeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
