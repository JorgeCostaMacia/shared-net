using FluentValidation;

namespace Shared.ValueObject.Domain;

public record OrderByValueObject : StringValueObject
{
    public OrderByValueObject(string value) : base(value) { }

    public static OrderByValueObject Create(string value, IValidator<OrderByValueObject>? validator = null)
    {
        OrderByValueObject ValueObject = new OrderByValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
}