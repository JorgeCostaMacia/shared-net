using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class PageNumberValueObject : IntValueObject
    {
        public PageNumberValueObject(int value) : base(value)
        {
        }

        public static new PageNumberValueObject From(int value, bool validate = true)
        {
            PageNumberValueObject ValueObject = new PageNumberValueObject(Convert(value));
            if (validate) new PageNumberValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new PageNumberValueObject From() => From(1);
        public static new PageNumberValueObject From(string value, bool validate = true) => From(Convert(value), validate);
        public static new PageNumberValueObject From(float value, bool validate = true) => From(Convert(value), validate);
        public static new PageNumberValueObject From(decimal value, bool validate = true) => From(Convert(value), validate);

        public static new IntValueObject? FromOrDefault(int? value, bool validate = true) => value != null && value != 0 ? From((int)value, validate) : From();
        public static new IntValueObject? FromOrDefault(string? value, bool validate = true) => value != null && value.Trim() != "" && value.Trim() != "0" ? From(value, validate) : From();
        public static new IntValueObject? FromOrDefault(float? value, bool validate = true) => value != null && value != 0 ? From((float)value, validate) : From();
        public static new IntValueObject? FromOrDefault(decimal? value, bool validate = true) => value != null && value != 0 ? From((decimal)value, validate) : From();
    }
}