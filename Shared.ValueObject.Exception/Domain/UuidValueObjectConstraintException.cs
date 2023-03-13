using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class UuidValueObjectConstraintException : IConstraintException<UuidValueObject>
    {
        public UuidValueObjectConstraintException(string property, UuidValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public UuidValueObjectConstraintException(string property, UuidValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public UuidValueObjectConstraintException(UuidValueObject value, List<string> constraints) : base("UuidValueObject", value, constraints, new Guid("82a064d0-39c8-4ac6-8cc2-d7de247c368f"), "UuidValueObject Constraint Exception")
        {
        }

        public UuidValueObjectConstraintException(UuidValueObject value, List<string> constraints, System.Exception inner) : base("UuidValueObject", value, constraints, new Guid("82a064d0-39c8-4ac6-8cc2-d7de247c368f"), "UuidValueObject Constraint Exception", inner)
        {
        }

        public UuidValueObjectConstraintException(UuidValueObject value, List<string> constraints, string message) : base("UuidValueObject", value, constraints, new Guid("82a064d0-39c8-4ac6-8cc2-d7de247c368f"), message)
        {
        }

        public UuidValueObjectConstraintException(UuidValueObject value, List<string> constraints, string message, System.Exception inner) : base("UuidValueObject", value, constraints, new Guid("82a064d0-39c8-4ac6-8cc2-d7de247c368f"), message, inner)
        {
        }

        public UuidValueObjectConstraintException(UuidValueObject value, List<string> constraints, Guid id, string message) : base("UuidValueObject", value, constraints, id, message)
        {
        }

        public UuidValueObjectConstraintException(UuidValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("UuidValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
