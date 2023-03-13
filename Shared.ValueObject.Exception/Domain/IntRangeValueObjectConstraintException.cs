using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class IntRangeValueObjectConstraintException : IConstraintException<IntRangeValueObject>
    {
        public IntRangeValueObjectConstraintException(string property, IntRangeValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public IntRangeValueObjectConstraintException(string property, IntRangeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public IntRangeValueObjectConstraintException(IntRangeValueObject value, List<string> constraints) : base("IntRangeValueObject", value, constraints, new Guid("67ec51e3-0abd-40ec-a356-21371cbc1f47"), "IntRangeValueObject Constraint Exception")
        {
        }

        public IntRangeValueObjectConstraintException(IntRangeValueObject value, List<string> constraints, System.Exception inner) : base("IntRangeValueObject", value, constraints, new Guid("67ec51e3-0abd-40ec-a356-21371cbc1f47"), "IntRangeValueObject Constraint Exception", inner)
        {
        }

        public IntRangeValueObjectConstraintException(IntRangeValueObject value, List<string> constraints, string message) : base("IntRangeValueObject", value, constraints, new Guid("67ec51e3-0abd-40ec-a356-21371cbc1f47"), message)
        {
        }

        public IntRangeValueObjectConstraintException(IntRangeValueObject value, List<string> constraints, string message, System.Exception inner) : base("IntRangeValueObject", value, constraints, new Guid("67ec51e3-0abd-40ec-a356-21371cbc1f47"), message, inner)
        {
        }

        public IntRangeValueObjectConstraintException(IntRangeValueObject value, List<string> constraints, Guid id, string message) : base("IntRangeValueObject", value, constraints, id, message)
        {
        }

        public IntRangeValueObjectConstraintException(IntRangeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("IntRangeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
