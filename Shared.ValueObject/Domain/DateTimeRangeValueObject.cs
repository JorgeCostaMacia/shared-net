namespace Shared.ValueObject.Domain
{
    public class DateTimeRangeValueObject : IValueObject
    {
        public DateTime ValueStart { get; }
        public DateTime ValueEnd { get; }

        public DateTimeRangeValueObject()
        {
            DateTime now = DateTime.UtcNow;

            ValueStart = now;
            ValueEnd = now;
        }

        public DateTimeRangeValueObject(DateTime valueStart, DateTime valueEnd)
        {
            ValueStart = valueStart.ToUniversalTime();
            ValueEnd = valueEnd.ToUniversalTime();
        }

        public DateTimeRangeValueObject(int valueStart, int valueEnd)
        {
            ValueStart = new DateTime(valueStart, DateTimeKind.Utc);
            ValueEnd = new DateTime(valueEnd, DateTimeKind.Utc);
        }

        public DateTimeRangeValueObject(float valueStart, float valueEnd)
        {
            ValueStart = new DateTime((int)valueStart, DateTimeKind.Utc);
            ValueEnd = new DateTime((int)valueEnd, DateTimeKind.Utc);
        }

        public DateTimeRangeValueObject(string valueStart, string valueEnd)
        {
            ValueStart = DateTime.Parse(valueStart).ToUniversalTime();
            ValueEnd = DateTime.Parse(valueEnd).ToUniversalTime();
        }
    }
}
