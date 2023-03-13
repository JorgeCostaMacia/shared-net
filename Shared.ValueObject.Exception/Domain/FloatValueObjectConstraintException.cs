using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class FloatValueObjectConstraintException : IConstraintException<FloatValueObject>
    {
        public FloatValueObjectConstraintException(string property, FloatValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public FloatValueObjectConstraintException(string property, FloatValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public FloatValueObjectConstraintException(FloatValueObject value, List<string> constraints) : base("FloatValueObject", value, constraints, new Guid("18bc336e-3322-40c9-a19d-e041238434b1"), "FloatValueObject Constraint Exception")
        {
        }

        public FloatValueObjectConstraintException(FloatValueObject value, List<string> constraints, System.Exception inner) : base("FloatValueObject", value, constraints, new Guid("18bc336e-3322-40c9-a19d-e041238434b1"), "FloatValueObject Constraint Exception", inner)
        {
        }

        public FloatValueObjectConstraintException(FloatValueObject value, List<string> constraints, string message) : base("FloatValueObject", value, constraints, new Guid("18bc336e-3322-40c9-a19d-e041238434b1"), message)
        {
        }

        public FloatValueObjectConstraintException(FloatValueObject value, List<string> constraints, string message, System.Exception inner) : base("FloatValueObject", value, constraints, new Guid("18bc336e-3322-40c9-a19d-e041238434b1"), message, inner)
        {
        }

        public FloatValueObjectConstraintException(FloatValueObject value, List<string> constraints, Guid id, string message) : base("FloatValueObject", value, constraints, id, message)
        {
        }

        public FloatValueObjectConstraintException(FloatValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("FloatValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
