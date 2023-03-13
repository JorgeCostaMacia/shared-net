namespace Shared.ValueObject.Domain
{
    public class DateTimeValueObject : IValueObject
    {
        public DateTime Value { get; }

        public DateTimeValueObject()
        {
            Value = DateTime.UtcNow;
        }

        public DateTimeValueObject(DateTime value)
        {
            Value = value.ToUniversalTime();
        }

        public DateTimeValueObject(int value)
        {
            Value = new DateTime(value, DateTimeKind.Utc);
        }

        public DateTimeValueObject(float value)
        {
            Value = new DateTime((int)value, DateTimeKind.Utc);
        }

        public DateTimeValueObject(string value)
        {
            Value = DateTime.Parse(value).ToUniversalTime();
        }
    }
}
