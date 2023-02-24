using Shared.Exception.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class UuidValueObjectConstraintException : IConstraintException<Guid>
    {
        public UuidValueObjectConstraintException(Guid value, List<string> constraints) : base("UuidValueObject", value, constraints, new Guid("82a064d0-39c8-4ac6-8cc2-d7de247c368f"), "UuidValueObject Constraint Exception")
        {
        }

        public UuidValueObjectConstraintException(Guid value, List<string> constraints, System.Exception inner) : base("UuidValueObject", value, constraints, new Guid("82a064d0-39c8-4ac6-8cc2-d7de247c368f"), "UuidValueObject Constraint Exception", inner)
        {
        }

        public UuidValueObjectConstraintException(Guid value, List<string> constraints, string message) : base("UuidValueObject", value, constraints, new Guid("82a064d0-39c8-4ac6-8cc2-d7de247c368f"), message)
        {
        }

        public UuidValueObjectConstraintException(Guid value, List<string> constraints, string message, System.Exception inner) : base("UuidValueObject", value, constraints, new Guid("82a064d0-39c8-4ac6-8cc2-d7de247c368f"), message, inner)
        {
        }

        public UuidValueObjectConstraintException(Guid value, List<string> constraints, Guid id, string message) : base("UuidValueObject", value, constraints, id, message)
        {
        }

        public UuidValueObjectConstraintException(Guid value, List<string> constraints, Guid id, string message, System.Exception inner) : base("UuidValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
