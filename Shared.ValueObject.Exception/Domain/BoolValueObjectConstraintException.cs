using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class BoolValueObjectConstraintException : IConstraintException<BoolValueObject>
    {
        public BoolValueObjectConstraintException(string property, BoolValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public BoolValueObjectConstraintException(string property, BoolValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public BoolValueObjectConstraintException(BoolValueObject value, List<string> constraints) : base("BoolValueObject", value, constraints, new Guid("15289aba-b342-4d9e-8a2e-65c42876d4b4"), "BoolValueObject Constraint Exception")
        {
        }

        public BoolValueObjectConstraintException(BoolValueObject value, List<string> constraints, System.Exception inner) : base("BoolValueObject", value, constraints, new Guid("510b83a615289aba-b342-4d9e-8a2e-65c42876d4b4"), "BoolValueObject Constraint Exception", inner)
        {
        }

        public BoolValueObjectConstraintException(BoolValueObject value, List<string> constraints, string message) : base("BoolValueObject", value, constraints, new Guid("15289aba-b342-4d9e-8a2e-65c42876d4b4"), message)
        {
        }

        public BoolValueObjectConstraintException(BoolValueObject value, List<string> constraints, string message, System.Exception inner) : base("BoolValueObject", value, constraints, new Guid("15289aba-b342-4d9e-8a2e-65c42876d4b4"), message, inner)
        {
        }

        public BoolValueObjectConstraintException(BoolValueObject value, List<string> constraints, Guid id, string message) : base("BoolValueObject", value, constraints, id, message)
        {
        }

        public BoolValueObjectConstraintException(BoolValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("BoolValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
