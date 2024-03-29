﻿using FluentValidation;

namespace Shared.ValueObject.Domain
{
    public class OrderByValueObject : StringValueObject
    {
        public OrderByValueObject(string value) : base(value)
        {
        }

        public static new OrderByValueObject From(string value, bool validate = true)
        {
            OrderByValueObject ValueObject = new OrderByValueObject(Convert(value));
            if (validate) new OrderByValueObjectValidator().ValidateAndThrow(ValueObject);

            return ValueObject;
        }

        protected static new string Convert(string value) => StringValueObject.Convert(value).ToUpper();
    }
}