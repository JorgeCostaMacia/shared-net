namespace Shared.ValueObject.Domain
{
    public class DateTimeValueObject : IValueObject
    {
        public DateTime Value { get; }


        public DateTimeValueObject()
        {
            Value = DateTime.Now;
        }

        public DateTimeValueObject(DateTime value)
        {
            Value = value;
        }

        public DateTimeValueObject(int value)
        {
            Value = new DateTime(1965, 1, 1, 0, 0, 0, 0).AddSeconds(value);
        }

        public DateTimeValueObject(float value)
        {
            Value = new DateTime(1965, 1, 1, 0, 0, 0, 0).AddSeconds(value);
        }

        public DateTimeValueObject(string value)
        {
            Value = DateTime.Parse(value);
        }
    }
}
