using System.Linq.Expressions;
using JorgeCostaMacia.ValueObject.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JorgeCostaMacia.ValueObject.EfConverter.Infrastructure;

/// <summary>
/// EF Core value converter that stores a <see cref="UriValueObject"/>-derived Value Object as the text its URI was
/// built from (<see cref="Uri.OriginalString"/>), and rehydrates it through its hydration constructor, unvalidated.
/// </summary>
/// <typeparam name="TValueObject">The URI Value Object type to convert.</typeparam>
public sealed class UriValueObjectConverter<TValueObject> : ValueConverter<TValueObject, string>
    where TValueObject : UriValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UriValueObjectConverter{TValueObject}"/> class.
    /// </summary>
    public UriValueObjectConverter() : base(v => v.Value.OriginalString, FromProvider()) { }

    // The column holds text; the hydration constructor takes a Uri. Rehydrating builds the Uri as RelativeOrAbsolute
    // so a stored value comes back as it was, without the validation a Create would apply.
    private static Expression<Func<string, TValueObject>> FromProvider()
    {
        Expression<Func<Uri, TValueObject>> construct = ValueObjectConstructor.From<TValueObject, Uri>();
        ParameterExpression value = Expression.Parameter(typeof(string), "value");
        NewExpression uri = Expression.New(
            typeof(Uri).GetConstructor(new Type[] { typeof(string), typeof(UriKind) })!,
            value,
            Expression.Constant(UriKind.RelativeOrAbsolute));

        return Expression.Lambda<Func<string, TValueObject>>(Expression.Invoke(construct, uri), value);
    }
}
