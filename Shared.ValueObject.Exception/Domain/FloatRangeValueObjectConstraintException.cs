using Shared.Exception.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class FloatRangeValueObjectConstraintException : IConstraintException<float>
    {
        public FloatRangeValueObjectConstraintException(float value, List<string> constraints) : base("FloatRangeValueObject", value, constraints, new Guid("13bad58b-3e41-4b31-9660-72934e5cc5da"), "FloatRangeValueObject Constraint Exception")
        {
        }

        public FloatRangeValueObjectConstraintException(float value, List<string> constraints, System.Exception inner) : base("FloatRangeValueObject", value, constraints, new Guid("13bad58b-3e41-4b31-9660-72934e5cc5da"), "FloatRangeValueObject Constraint Exception", inner)
        {
        }

        public FloatRangeValueObjectConstraintException(float value, List<string> constraints, string message) : base("FloatRangeValueObject", value, constraints, new Guid("13bad58b-3e41-4b31-9660-72934e5cc5da"), message)
        {
        }

        public FloatRangeValueObjectConstraintException(float value, List<string> constraints, string message, System.Exception inner) : base("FloatRangeValueObject", value, constraints, new Guid("13bad58b-3e41-4b31-9660-72934e5cc5da"), message, inner)
        {
        }

        public FloatRangeValueObjectConstraintException(float value, List<string> constraints, Guid id, string message) : base("FloatRangeValueObject", value, constraints, id, message)
        {
        }

        public FloatRangeValueObjectConstraintException(float value, List<string> constraints, Guid id, string message, System.Exception inner) : base("FloatRangeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
