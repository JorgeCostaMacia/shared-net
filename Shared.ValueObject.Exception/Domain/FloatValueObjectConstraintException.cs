using Shared.Exception.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class FloatValueObjectConstraintException : IConstraintException<float>
    {
        public FloatValueObjectConstraintException(float value, List<string> constraints) : base("FloatValueObject", value, constraints, new Guid("18bc336e-3322-40c9-a19d-e041238434b1"), "FloatValueObject Constraint Exception")
        {
        }

        public FloatValueObjectConstraintException(float value, List<string> constraints, System.Exception inner) : base("FloatValueObject", value, constraints, new Guid("18bc336e-3322-40c9-a19d-e041238434b1"), "FloatValueObject Constraint Exception", inner)
        {
        }

        public FloatValueObjectConstraintException(float value, List<string> constraints, string message) : base("FloatValueObject", value, constraints, new Guid("18bc336e-3322-40c9-a19d-e041238434b1"), message)
        {
        }

        public FloatValueObjectConstraintException(float value, List<string> constraints, string message, System.Exception inner) : base("FloatValueObject", value, constraints, new Guid("18bc336e-3322-40c9-a19d-e041238434b1"), message, inner)
        {
        }

        public FloatValueObjectConstraintException(float value, List<string> constraints, Guid id, string message) : base("FloatValueObject", value, constraints, id, message)
        {
        }

        public FloatValueObjectConstraintException(float value, List<string> constraints, Guid id, string message, System.Exception inner) : base("FloatValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
