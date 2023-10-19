using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class PageSizeValueObject : IntValueObject
    {
        public PageSizeValueObject(int value) : base(value)
        {
        }

        public static new PageSizeValueObject From(int value, bool validate = true)
        {
            PageSizeValueObject ValueObject = new PageSizeValueObject(Convert(value));
            if (validate) new PageSizeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new PageSizeValueObject From() => From(10000);
        public static new PageSizeValueObject From(string value, bool validate = true) => From(Convert(value), validate);
        public static new PageSizeValueObject From(float value, bool validate = true) => From(Convert(value), validate);
        public static new PageSizeValueObject From(decimal value, bool validate = true) => From(Convert(value), validate);

        public static new PageSizeValueObject? FromOrDefault(int? value, bool validate = true) => value != null && value != 0 ? From((int)value, validate) : From();
        public static new PageSizeValueObject? FromOrDefault(string? value, bool validate = true) => value != null && value.Trim() != "" && value.Trim() != "0" ? From(value, validate) : From();
        public static new PageSizeValueObject? FromOrDefault(float? value, bool validate = true) => value != null && value != 0 ? From((float)value, validate) : From();
        public static new PageSizeValueObject? FromOrDefault(decimal? value, bool validate = true) => value != null && value != 0 ? From((decimal)value, validate) : From();
    }
}