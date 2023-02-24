namespace Shared.ValueObject.Domain
{
    public class StringValueObject : IValueObject
    {
        public string Value { get; }

        public StringValueObject()
        {
            Value = "";
        }

        public StringValueObject(string value)
        {
            Value = value.Trim();
        }

        public StringValueObject(int value)
        {
            Value = value.ToString();
        }

        public StringValueObject(float value)
        {
            Value = value.ToString();
        }

        public StringValueObject(bool value)
        {
            Value = value.ToString();
        }

        public StringValueObject(DateTime value)
        {
            Value = value.ToString();
        }

        public StringValueObject(Guid value)
        {
            Value = value.ToString();
        }
    }
}
