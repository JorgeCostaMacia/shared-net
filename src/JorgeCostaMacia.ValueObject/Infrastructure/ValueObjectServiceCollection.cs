using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using JorgeCostaMacia.ValueObject.Domain;

namespace JorgeCostaMacia.ValueObject.Infrastructure;

/// <summary>
/// Provides extension methods for <see cref="IServiceCollection"/> to register all
/// FluentValidation validators for the Domain Value Objects within the application's
/// Dependency Injection container.
/// </summary>
public static class ValueObjectServiceCollection
{
    /// <summary>
    /// Registers all Value Object validators from the Domain layer as scoped services.
    /// </summary>
    /// <remarks>
    /// Each registration maps the generic <see cref="IValidator{T}"/> interface to its concrete
    /// implementation (e.g., <see cref="IValidator{BoolValueObject}"/> maps to <see cref="BoolValueObjectValidator"/>).
    /// This ensures that FluentValidation's validation pipeline can correctly resolve the necessary
    /// validators when injecting <see cref="IValidator{T}"/> or using validation middleware.
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
    public static IServiceCollection AddValueObjectServiceCollection(this IServiceCollection services)
    {
        services
           .AddScoped<IValidator<BoolValueObject>, BoolValueObjectValidator>()
           .AddScoped<IValidator<ByteValueObject>, ByteValueObjectValidator>()
           .AddScoped<IValidator<DateTimeRangeValueObject>, DateTimeRangeValueObjectValidator>()
           .AddScoped<IValidator<DateTimeValueObject>, DateTimeValueObjectValidator>()
           .AddScoped<IValidator<DecimalValueObject>, DecimalValueObjectValidator>()
           .AddScoped<IValidator<EmailValueObject>, EmailValueObjectValidator>()
           .AddScoped<IValidator<FloatRangeValueObject>, FloatRangeValueObjectValidator>()
           .AddScoped<IValidator<FloatValueObject>, FloatValueObjectValidator>()
           .AddScoped<IValidator<GroupByValueObject>, GroupByValueObjectValidator>()
           .AddScoped<IValidator<IntRangeValueObject>, IntRangeValueObjectValidator>()
           .AddScoped<IValidator<IntValueObject>, IntValueObjectValidator>()
           .AddScoped<IValidator<IpValueObject>, IpValueObjectValidator>()
           .AddScoped<IValidator<JsonValueObject>, JsonValueObjectValidator>()
           .AddScoped<IValidator<OrderByValueObject>, OrderByValueObjectValidator>()
           .AddScoped<IValidator<OrderTypeValueObject>, OrderTypeValueObjectValidator>()
           .AddScoped<IValidator<PageNumberRangeValueObject>, PageNumberRangeValueObjectValidator>()
           .AddScoped<IValidator<PageNumberValueObject>, PageNumberValueObjectValidator>()
           .AddScoped<IValidator<PageSizeValueObject>, PageSizeValueObjectValidator>()
           .AddScoped<IValidator<StringValueObject>, StringValueObjectValidator>()
           .AddScoped<IValidator<UrlValueObject>, UrlValueObjectValidator>()
           .AddScoped<IValidator<UuidValueObject>, UuidValueObjectValidator>()
           .AddScoped<IValidator<DoubleValueObject>, DoubleValueObjectValidator>()
           .AddScoped<IValidator<LongValueObject>, LongValueObjectValidator>()
           .AddScoped<IValidator<DateTimeUtcValueObject>, DateTimeUtcValueObjectValidator>();

        return services;
    }
}