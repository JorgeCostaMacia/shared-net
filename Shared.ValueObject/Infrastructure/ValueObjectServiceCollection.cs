using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.ValueObject.Domain;

namespace Shared.ValueObject.Infrastructure;

public static class ValueObjectServiceCollection
{
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
            .AddScoped<IValidator<UuidValueObject>, UuidValueObjectValidator>();

        return services;
    }
}