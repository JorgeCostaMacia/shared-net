using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class FloatRangeValueObjectConstraintException : IConstraintException<FloatRangeValueObject>
    {
        public FloatRangeValueObjectConstraintException(string property, FloatRangeValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public FloatRangeValueObjectConstraintException(string property, FloatRangeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public FloatRangeValueObjectConstraintException(FloatRangeValueObject value, List<string> constraints) : base("FloatRangeValueObject", value, constraints, new Guid("13bad58b-3e41-4b31-9660-72934e5cc5da"), "FloatRangeValueObject Constraint Exception")
        {
        }

        public FloatRangeValueObjectConstraintException(FloatRangeValueObject value, List<string> constraints, System.Exception inner) : base("FloatRangeValueObject", value, constraints, new Guid("13bad58b-3e41-4b31-9660-72934e5cc5da"), "FloatRangeValueObject Constraint Exception", inner)
        {
        }

        public FloatRangeValueObjectConstraintException(FloatRangeValueObject value, List<string> constraints, string message) : base("FloatRangeValueObject", value, constraints, new Guid("13bad58b-3e41-4b31-9660-72934e5cc5da"), message)
        {
        }

        public FloatRangeValueObjectConstraintException(FloatRangeValueObject value, List<string> constraints, string message, System.Exception inner) : base("FloatRangeValueObject", value, constraints, new Guid("13bad58b-3e41-4b31-9660-72934e5cc5da"), message, inner)
        {
        }

        public FloatRangeValueObjectConstraintException(FloatRangeValueObject value, List<string> constraints, Guid id, string message) : base("FloatRangeValueObject", value, constraints, id, message)
        {
        }

        public FloatRangeValueObjectConstraintException(FloatRangeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("FloatRangeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
