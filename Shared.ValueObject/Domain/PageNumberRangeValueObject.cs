using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class PageNumberRangeValueObject : IntRangeValueObject
    {
        public PageNumberRangeValueObject(PageNumberValueObject valueStart, PageNumberValueObject valueEnd) : base(valueStart, valueEnd)
        {
        }

        public static new PageNumberRangeValueObject From(int valueStart, int valueEnd, bool validate = true)
        {
            PageNumberRangeValueObject ValueObject = new PageNumberRangeValueObject(PageNumberValueObject.From(valueStart, false), PageNumberValueObject.From(valueEnd, false));
            if (validate) new PageNumberRangeValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        public static new PageNumberRangeValueObject From() => From(1, 1);
        public static PageNumberRangeValueObject From(PageNumberValueObject valueStart, PageNumberValueObject valueEnd, bool validate = true) => From(valueStart.Value, valueEnd.Value, validate);
        public static new PageNumberRangeValueObject From(string valueStart, string valueEnd, bool validate = true) => From(PageNumberValueObject.From(valueStart, false), PageNumberValueObject.From(valueEnd, false), validate);
        public static new PageNumberRangeValueObject From(float valueStart, float valueEnd, bool validate = true) => From(PageNumberValueObject.From(valueStart, false), PageNumberValueObject.From(valueEnd, false), validate);
        public static new PageNumberRangeValueObject From(decimal valueStart, decimal valueEnd, bool validate = true) => From(PageNumberValueObject.From(valueStart, false), PageNumberValueObject.From(valueEnd, false), validate);

        public static new PageNumberRangeValueObject? FromOrDefault(int? valueStart, int? valueEnd, bool validate = true) => valueStart != null && valueEnd != null ? From((int)valueStart, (int)valueEnd, validate) : From();
        public static PageNumberRangeValueObject? FromOrDefault(PageNumberValueObject? valueStart, PageNumberValueObject? valueEnd, bool validate = true) => valueStart != null && valueEnd != null ? From(valueStart, valueEnd, validate) : From();
        public static new PageNumberRangeValueObject? FromOrDefault(string? valueStart, string? valueEnd, bool validate = true) => valueStart != null && valueEnd != null && valueStart != "" && valueEnd != "" ? From(valueStart, valueEnd, validate) : From();
        public static new PageNumberRangeValueObject? FromOrDefault(float? valueStart, float? valueEnd, bool validate = true) => valueStart != null && valueEnd != null ? From((float)valueStart, (float)valueEnd, validate) : From();
        public static new PageNumberRangeValueObject? FromOrDefault(decimal? valueStart, decimal? valueEnd, bool validate = true) => valueStart != null && valueEnd != null ? From((decimal)valueStart, (decimal)valueEnd, validate) : From();
    }
}
