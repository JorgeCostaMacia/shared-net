using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class FloatRangeValueObject : RangeValueObject<float>
    {
        public FloatRangeValueObject(float valueStart, float valueEnd) : base(valueStart, valueEnd)
        {
        }

        public static new FloatRangeValueObject Create(float valueStart, float valueEnd, bool validate = true)
        {
            FloatRangeValueObject ValueObject = new FloatRangeValueObject(ToValue(valueStart), ToValue(valueEnd));
            if (validate) new FloatRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static FloatRangeValueObject Create() => new FloatRangeValueObject(0, 0);
        public static FloatRangeValueObject Create(FloatValueObject valueStart, FloatValueObject valueEnd, bool validate = true) => Create(valueStart.Value, valueEnd.Value, validate);

        protected static float ToValue(float value) => value;
    }
}
