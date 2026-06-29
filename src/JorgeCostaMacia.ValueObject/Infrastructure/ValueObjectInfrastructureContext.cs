using FluentValidation;
using JorgeCostaMacia.ValueObject.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace JorgeCostaMacia.ValueObject.Infrastructure;

/// <summary>
/// Infrastructure-layer service registrations for the package. Composed by
/// <see cref="ValueObjectContext"/>; not part of the public API.
/// </summary>
internal static class ValueObjectInfrastructureContext
{
    /// <summary>
    /// Registers every Value Object's FluentValidation validator as a scoped <see cref="IValidator{T}"/>,
    /// mapped to its concrete implementation, so they can be resolved through the validation pipeline.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance so that additional calls can be chained.</returns>
    internal static IServiceCollection AddValueObjectInfrastructureContext(this IServiceCollection services)
    {
        services
           .AddScoped<IValidator<BoolValueObject>, BoolValueObjectValidator>()
           .AddScoped<IValidator<ByteValueObject>, ByteValueObjectValidator>()
           .AddScoped<IValidator<DateTimeRangeValueObject>, DateTimeRangeValueObjectValidator>()
           .AddScoped<IValidator<DateTimeUtcValueObject>, DateTimeUtcValueObjectValidator>()
           .AddScoped<IValidator<DateTimeValueObject>, DateTimeValueObjectValidator>()
           .AddScoped<IValidator<DecimalValueObject>, DecimalValueObjectValidator>()
           .AddScoped<IValidator<DoubleValueObject>, DoubleValueObjectValidator>()
           .AddScoped<IValidator<EmailValueObject>, EmailValueObjectValidator>()
           .AddScoped<IValidator<FloatRangeValueObject>, FloatRangeValueObjectValidator>()
           .AddScoped<IValidator<FloatValueObject>, FloatValueObjectValidator>()
           .AddScoped<IValidator<GroupByValueObject>, GroupByValueObjectValidator>()
           .AddScoped<IValidator<IntRangeValueObject>, IntRangeValueObjectValidator>()
           .AddScoped<IValidator<IntValueObject>, IntValueObjectValidator>()
           .AddScoped<IValidator<IpValueObject>, IpValueObjectValidator>()
           .AddScoped<IValidator<JsonValueObject>, JsonValueObjectValidator>()
           .AddScoped<IValidator<LongValueObject>, LongValueObjectValidator>()
           .AddScoped<IValidator<OrderByValueObject>, OrderByValueObjectValidator>()
           .AddScoped<IValidator<OrderTypeValueObject>, OrderTypeValueObjectValidator>()
           .AddScoped<IValidator<PageNumberRangeValueObject>, PageNumberRangeValueObjectValidator>()
           .AddScoped<IValidator<PageNumberValueObject>, PageNumberValueObjectValidator>()
           .AddScoped<IValidator<PageSizeValueObject>, PageSizeValueObjectValidator>()
           .AddScoped<IValidator<StringValueObject>, StringValueObjectValidator>()
           .AddScoped<IValidator<UrlValueObject>, UrlValueObjectValidator>()
           .AddScoped<IValidator<UuidValueObject>, UuidValueObjectValidator>();

        return services;
    }
}
