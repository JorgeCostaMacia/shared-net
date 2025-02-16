using FluentValidation;

namespace Shared.ValueObject.Domain;

public record GroupByValueObject : StringValueObject
{
    public GroupByValueObject(string value) : base(value) { }

    public GroupByValueObject Validate(IValidator<GroupByValueObject> validator)
    {
        validator.ValidateAndThrow(this);

        return this;
    }

    public static GroupByValueObject Create(string value) => new GroupByValueObject(Convert(value));

    protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
}