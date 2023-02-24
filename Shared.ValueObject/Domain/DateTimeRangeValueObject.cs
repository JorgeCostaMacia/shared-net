namespace Shared.ValueObject.Domain
{
    public class DateTimeRangeValueObject : IValueObject
    {
        public DateTime ValueStart { get; }
        public DateTime ValueEnd { get; }

        public DateTimeRangeValueObject()
        {
            var now = DateTime.Now;

            ValueStart = now;
            ValueEnd = now;
        }

        public DateTimeRangeValueObject(DateTime valueStart, DateTime valueEnd)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
        }

        public DateTimeRangeValueObject(int valueStart, int valueEnd)
        {
            ValueStart = new DateTime(1965, 1, 1, 0, 0, 0, 0).AddSeconds(valueStart);
            ValueEnd = new DateTime(1965, 1, 1, 0, 0, 0, 0).AddSeconds(valueEnd);
        }

        public DateTimeRangeValueObject(float valueStart, float valueEnd)
        {
            ValueStart = new DateTime(1965, 1, 1, 0, 0, 0, 0).AddSeconds(valueStart);
            ValueEnd = new DateTime(1965, 1, 1, 0, 0, 0, 0).AddSeconds(valueEnd);
        }

        public DateTimeRangeValueObject(string valueStart, string valueEnd)
        {
            ValueStart = DateTime.Parse(valueStart);
            ValueEnd = DateTime.Parse(valueEnd);
        }
    }
}
