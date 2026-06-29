using JorgeCostaMacia.ValueObject.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace JorgeCostaMacia.ValueObject;

/// <summary>
/// Composition root for the <c>JorgeCostaMacia.ValueObject</c> package: the single public entry point
/// that wires everything the package provides into the application's dependency injection container,
/// composing the package's internal per-layer contexts.
/// </summary>
public static class ValueObjectContext
{
    /// <summary>
    /// Registers every service exposed by the package — currently the FluentValidation validators for
    /// all Value Objects.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
    public static IServiceCollection AddValueObjectContext(this IServiceCollection services)
    {
        services
            .AddValueObjectInfrastructureContext();

        return services;
    }
}
