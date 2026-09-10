using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace JorgeCostaMacia.ValueObject.Tests;

/// <summary>
/// The DI seam: <c>AddValueObjectContext</c> is the one call a consumer makes, and every value object
/// in the library has to come out of the container resolvable. A validator added to the library but
/// forgotten in the registration is a runtime failure in a consumer — this catches it here, and it
/// scans the assembly rather than repeating the list, so the two cannot drift apart.
/// </summary>
public class ValueObjectContextTests
{
    private static readonly Type[] _valueObjects = typeof(IValueObject).Assembly.GetTypes()
        .Where(type => type is { IsClass: true, IsAbstract: false } && typeof(IValueObject).IsAssignableFrom(type))
        .OrderBy(type => type.Name, StringComparer.Ordinal)
        .ToArray();

    [Fact]
    public void AddValueObjectContext_RegistersAValidatorForEveryValueObject()
    {
        ServiceProvider provider = new ServiceCollection().AddValueObjectContext().BuildServiceProvider();

        using IServiceScope scope = provider.CreateScope();
        List<string> missing = new List<string>();
        foreach (Type valueObject in _valueObjects)
        {
            if (scope.ServiceProvider.GetService(typeof(IValidator<>).MakeGenericType(valueObject)) is null)
            {
                missing.Add(valueObject.Name);
            }
        }

        Assert.True(missing.Count == 0, "No IValidator<> registered for:" + Environment.NewLine + string.Join(Environment.NewLine, missing));
    }

    [Fact]
    public void AddValueObjectContext_ReturnsTheSameCollection_SoItChains()
    {
        IServiceCollection services = new ServiceCollection();

        Assert.Same(services, services.AddValueObjectContext());
    }

    // Scoped, not singleton: a validator composes the ones it includes, and those follow the scope
    // of whatever the consumer resolves them in.
    [Fact]
    public void AddValueObjectContext_RegistersTheValidatorsAsScoped()
    {
        ServiceCollection services = new ServiceCollection();
        services.AddValueObjectContext();

        ServiceDescriptor descriptor = Assert.Single(
            services,
            service => service.ServiceType == typeof(IValidator<StringValueObject>));

        Assert.Equal(ServiceLifetime.Scoped, descriptor.Lifetime);
    }
}
