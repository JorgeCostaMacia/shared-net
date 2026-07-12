using System.Reflection;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Tests;

/// <summary>
/// Contract test over the whole library: every value object must expose the creation surface the rest
/// of the ecosystem relies on — a public constructor (hydration, used by the EF converters and
/// deserializers) plus exactly one public static <c>From</c> and <c>Create</c> returning its own type.
/// A value object added without them is caught here, not in a consumer at runtime.
/// </summary>
public class ValueObjectCreationSurfaceTests
{
    private static readonly Type[] ValueObjects = typeof(IValueObject).Assembly.GetTypes()
        .Where(type => type is { IsClass: true, IsAbstract: false } && typeof(IValueObject).IsAssignableFrom(type))
        .ToArray();

    [Fact]
    public void EveryValueObject_ExposesTheCreationSurface()
    {
        Assert.NotEmpty(ValueObjects);   // guard: the scan really found the value objects

        List<string> violations = [];
        foreach (Type valueObject in ValueObjects)
        {
            if (valueObject.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Length == 0)
            {
                violations.Add($"{valueObject.Name}: no public constructor");
            }

            if (!DeclaresSingleFactory(valueObject, "From"))
            {
                violations.Add($"{valueObject.Name}: needs exactly one public static 'From' returning {valueObject.Name}");
            }

            if (!DeclaresSingleFactory(valueObject, "Create"))
            {
                violations.Add($"{valueObject.Name}: needs exactly one public static 'Create' returning {valueObject.Name}");
            }
        }

        Assert.True(violations.Count == 0, "Value object creation-surface violations:" + Environment.NewLine + string.Join(Environment.NewLine, violations));
    }

    /// <summary>The type declares exactly one public static factory of the given name that returns the type itself.</summary>
    private static bool DeclaresSingleFactory(Type valueObject, string name)
        => valueObject.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Count(method => method.Name == name && method.ReturnType == valueObject) == 1;
}
