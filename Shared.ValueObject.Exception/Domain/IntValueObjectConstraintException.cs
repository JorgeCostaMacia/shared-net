using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class IntValueObjectConstraintException : IConstraintException<IntValueObject>
    {
        public IntValueObjectConstraintException(string property, IntValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public IntValueObjectConstraintException(string property, IntValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public IntValueObjectConstraintException(IntValueObject value, List<string> constraints) : base("IntValueObject", value, constraints, new Guid("f93ef42d-4b96-4583-a55b-cad3ed54cffb"), "IntValueObject Constraint Exception")
        {
        }

        public IntValueObjectConstraintException(IntValueObject value, List<string> constraints, System.Exception inner) : base("IntValueObject", value, constraints, new Guid("f93ef42d-4b96-4583-a55b-cad3ed54cffb"), "IntValueObject Constraint Exception", inner)
        {
        }

        public IntValueObjectConstraintException(IntValueObject value, List<string> constraints, string message) : base("IntValueObject", value, constraints, new Guid("f93ef42d-4b96-4583-a55b-cad3ed54cffb"), message)
        {
        }

        public IntValueObjectConstraintException(IntValueObject value, List<string> constraints, string message, System.Exception inner) : base("IntValueObject", value, constraints, new Guid("f93ef42d-4b96-4583-a55b-cad3ed54cffb"), message, inner)
        {
        }

        public IntValueObjectConstraintException(IntValueObject value, List<string> constraints, Guid id, string message) : base("IntValueObject", value, constraints, id, message)
        {
        }

        public IntValueObjectConstraintException(IntValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("IntValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
