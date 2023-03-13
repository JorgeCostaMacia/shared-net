using Shared.Exception.Domain;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class DateTimeRangeValueObjectConstraintException : IConstraintException<DateTimeRangeValueObject>
    {
        public DateTimeRangeValueObjectConstraintException(string property, DateTimeRangeValueObject value, List<string> constraints, Guid id, string message) : base(property, value, constraints, id, message)
        {
        }

        public DateTimeRangeValueObjectConstraintException(string property, DateTimeRangeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base(property, value, constraints, id, message, inner)
        {
        }

        public DateTimeRangeValueObjectConstraintException(DateTimeRangeValueObject value, List<string> constraints) : base("DateTimeRangeValueObject", value, constraints, new Guid("cbb92bf8-887e-4adc-a255-bf87aacd73ce"), "DateTimeRangeValueObject Constraint Exception")
        {
        }

        public DateTimeRangeValueObjectConstraintException(DateTimeRangeValueObject value, List<string> constraints, System.Exception inner) : base("DateTimeRangeValueObject", value, constraints, new Guid("cbb92bf8-887e-4adc-a255-bf87aacd73ce"), "DateTimeRangeValueObject Constraint Exception", inner)
        {
        }

        public DateTimeRangeValueObjectConstraintException(DateTimeRangeValueObject value, List<string> constraints, string message) : base("DateTimeRangeValueObject", value, constraints, new Guid("cbb92bf8-887e-4adc-a255-bf87aacd73ce"), message)
        {
        }

        public DateTimeRangeValueObjectConstraintException(DateTimeRangeValueObject value, List<string> constraints, string message, System.Exception inner) : base("DateTimeRangeValueObject", value, constraints, new Guid("cbb92bf8-887e-4adc-a255-bf87aacd73ce"), message, inner)
        {
        }

        public DateTimeRangeValueObjectConstraintException(DateTimeRangeValueObject value, List<string> constraints, Guid id, string message) : base("DateTimeRangeValueObject", value, constraints, id, message)
        {
        }

        public DateTimeRangeValueObjectConstraintException(DateTimeRangeValueObject value, List<string> constraints, Guid id, string message, System.Exception inner) : base("DateTimeRangeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
