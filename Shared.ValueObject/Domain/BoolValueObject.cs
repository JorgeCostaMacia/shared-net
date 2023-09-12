using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class BoolValueObject : IValueObject
    {
        public bool Value { get; }

        public BoolValueObject(bool value)
        {
            Value = value;
        }

        public static BoolValueObject Create(bool value, bool validate = true)
        {
            BoolValueObject ValueObject = new BoolValueObject(ToValue(value));
            if (validate) new BoolValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static BoolValueObject Create() => new BoolValueObject(true);
        public static BoolValueObject Create(int value, bool validate = true) => Create(ToValue(value), validate);
        public static BoolValueObject Create(float value, bool validate = true) => Create(ToValue(value), validate);
        public static BoolValueObject Create(string value, bool validate = true) => Create(ToValue(value), validate);

        protected static bool ToValue(bool value) => value;
        protected static bool ToValue(int value) => value == 1;
        protected static bool ToValue(float value) => (int)value == 1;
        protected static bool ToValue(string value) => value.ToUpper() == "TRUE" || value.ToUpper() == "1" || value.ToUpper() == "SI" || value.ToUpper() == "YES";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
