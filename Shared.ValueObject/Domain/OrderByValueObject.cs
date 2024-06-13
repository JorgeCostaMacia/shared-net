using FluentValidation;

namespace Shared.ValueObject.Domain;

public class OrderByValueObject(string value) : StringValueObject(value)
{
    public static OrderByValueObject Create(string value, IValidator<OrderByValueObject>? validator = null)
    {
        OrderByValueObject ValueObject = new OrderByValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
}