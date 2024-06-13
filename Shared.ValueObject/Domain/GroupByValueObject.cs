using FluentValidation;

namespace Shared.ValueObject.Domain;

public class GroupByValueObject(string value) : StringValueObject(value)
{

    public static GroupByValueObject Create(string value, IValidator<GroupByValueObject>? validator = null)
    {
        GroupByValueObject ValueObject = new GroupByValueObject(Convert(value));
        validator?.ValidateAndThrow(ValueObject);

        return ValueObject;
    }

    protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
}