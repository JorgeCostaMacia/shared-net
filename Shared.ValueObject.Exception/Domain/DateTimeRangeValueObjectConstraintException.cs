using Shared.Exception.Domain;

namespace Shared.ValueObject.Exception.Domain
{
    public class DateTimeRangeValueObjectConstraintException : IConstraintException<DateTime>
    {
        public DateTimeRangeValueObjectConstraintException(DateTime value, List<string> constraints) : base("DateTimeRangeValueObject", value, constraints, new Guid("cbb92bf8-887e-4adc-a255-bf87aacd73ce"), "DateTimeRangeValueObject Constraint Exception")
        {
        }

        public DateTimeRangeValueObjectConstraintException(DateTime value, List<string> constraints, System.Exception inner) : base("DateTimeRangeValueObject", value, constraints, new Guid("cbb92bf8-887e-4adc-a255-bf87aacd73ce"), "DateTimeRangeValueObject Constraint Exception", inner)
        {
        }

        public DateTimeRangeValueObjectConstraintException(DateTime value, List<string> constraints, string message) : base("DateTimeRangeValueObject", value, constraints, new Guid("cbb92bf8-887e-4adc-a255-bf87aacd73ce"), message)
        {
        }

        public DateTimeRangeValueObjectConstraintException(DateTime value, List<string> constraints, string message, System.Exception inner) : base("DateTimeRangeValueObject", value, constraints, new Guid("cbb92bf8-887e-4adc-a255-bf87aacd73ce"), message, inner)
        {
        }

        public DateTimeRangeValueObjectConstraintException(DateTime value, List<string> constraints, Guid id, string message) : base("DateTimeRangeValueObject", value, constraints, id, message)
        {
        }

        public DateTimeRangeValueObjectConstraintException(DateTime value, List<string> constraints, Guid id, string message, System.Exception inner) : base("DateTimeRangeValueObject", value, constraints, id, message, inner)
        {
        }
    }
}
