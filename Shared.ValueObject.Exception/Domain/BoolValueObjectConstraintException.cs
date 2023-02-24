using Shared.Exception.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class BoolValueObjectConstraintException : IConstraintException<bool>
    {
        public BoolValueObjectConstraintException(bool value, List<string> constraints) : base("BoolValueObject", value, constraints, new Guid("15289aba-b342-4d9e-8a2e-65c42876d4b4"), "BoolValueObject Constraint Exception")
        {
        }

        public BoolValueObjectConstraintException(bool value, List<string> constraints, System.Exception inner) : base("BoolValueObject", value, constraints, new Guid("510b83a615289aba-b342-4d9e-8a2e-65c42876d4b4"), "BoolValueObject Constraint Exception", inner)
        {
        }

        public BoolValueObjectConstraintException(bool value, List<string> constraints, string message) : base("BoolValueObject", value, constraints, new Guid("15289aba-b342-4d9e-8a2e-65c42876d4b4"), message)
        {
        }

        public BoolValueObjectConstraintException(bool value, List<string> constraints, string message, System.Exception inner) : base("BoolValueObject", value, constraints, new Guid("15289aba-b342-4d9e-8a2e-65c42876d4b4"), message, inner)
        {
        }

        public BoolValueObjectConstraintException(bool value, List<string> constraints, Guid id, string message) : base("BoolValueObject", value, constraints, id, message)
        {
        }

        public BoolValueObjectConstraintException(bool value, List<string> constraints, Guid id, string message, System.Exception inner) : base("BoolValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
