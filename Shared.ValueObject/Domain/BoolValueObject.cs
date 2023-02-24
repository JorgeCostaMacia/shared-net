namespace Shared.ValueObject.Domain
{
    public class BoolValueObject : IValueObject
    {
        public bool Value { get; }

        public BoolValueObject()
        {
            Value = true;
        }

        public BoolValueObject(bool value)
        {
            Value = value;
        }

        public BoolValueObject(int value)
        {
            Value = value == 1;
        }

        public BoolValueObject(float value)
        {
            Value = (int)value == 1;
        }

        public BoolValueObject(string value)
        {
            Value = value.ToUpper() == "TRUE" || value.ToUpper() == "1" || value.ToUpper() == "SI";
        }
    }
}
