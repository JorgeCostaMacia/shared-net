using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Validator.Domain;

namespace Shared.ValueObject.Validator.Infrastructure
{
    public static class ValidatorServiceCollection
    {
        //public static IServiceCollection AddValueObjectValidatorService(this IServiceCollection services) => services;
        public static IServiceCollection AddValueObjectValidatorService(this IServiceCollection services, IConfiguration config) => services;

        public static IServiceCollection AddVReproServicesNsCanProService(this IServiceCollection services)
        {
            services
                .AddScoped<IValidator<BoolValueObject>, BoolValueObjectValidator>()
                .AddScoped<IValidator<DateTimeRangeValueObject>, DateTimeRangeValueObjectValidator>()
                .AddScoped<IValidator<DateTimeValueObject>, DateTimeValueObjectValidator>()
                .AddScoped<IValidator<FloatRangeValueObject>, FloatRangeValueObjectValidator>()
                .AddScoped<IValidator<FloatValueObject>, FloatValueObjectValidator>()
                .AddScoped<IValidator<GroupByValueObject>, GroupByValueObjectValidator>()
                .AddScoped<IValidator<GroupTypeValueObject>, GroupTypeValueObjectValidator>()
                .AddScoped<IValidator<IntRangeValueObject>, IntRangeValueObjectValidator>()
                .AddScoped<IValidator<IntValueObject>, IntValueObjectValidator>()
                .AddScoped<IValidator<ListValueObject<BoolValueObject>>, ListValueObjectValidator<BoolValueObject>>()
                .AddScoped<IValidator<ListValueObject<DateTimeRangeValueObject>>, ListValueObjectValidator<DateTimeRangeValueObject>>()
                .AddScoped<IValidator<ListValueObject<DateTimeValueObject>>, ListValueObjectValidator<DateTimeValueObject>>()
                .AddScoped<IValidator<ListValueObject<FloatRangeValueObject>>, ListValueObjectValidator<FloatRangeValueObject>>()
                .AddScoped<IValidator<ListValueObject<FloatValueObject>>, ListValueObjectValidator<FloatValueObject>>()
                .AddScoped<IValidator<ListValueObject<GroupByValueObject>>, ListValueObjectValidator<GroupByValueObject>>()
                .AddScoped<IValidator<ListValueObject<GroupTypeValueObject>>, ListValueObjectValidator<GroupTypeValueObject>>()
                .AddScoped<IValidator<ListValueObject<IntRangeValueObject>>, ListValueObjectValidator<IntRangeValueObject>>()
                .AddScoped<IValidator<ListValueObject<IntValueObject>>, ListValueObjectValidator<IntValueObject>>()
                .AddScoped<IValidator<ListValueObject<OrderByValueObject>>, ListValueObjectValidator<OrderByValueObject>>()
                .AddScoped<IValidator<ListValueObject<OrderTypeValueObject>>, ListValueObjectValidator<OrderTypeValueObject>>()
                .AddScoped<IValidator<ListValueObject<PageNumberRangeValueObject>>, ListValueObjectValidator<PageNumberRangeValueObject>>()
                .AddScoped<IValidator<ListValueObject<PageNumberValueObject>>, ListValueObjectValidator<PageNumberValueObject>>()
                .AddScoped<IValidator<ListValueObject<PageSizeValueObject>>, ListValueObjectValidator<PageSizeValueObject>>()
                .AddScoped<IValidator<ListValueObject<StringValueObject>>, ListValueObjectValidator<StringValueObject>>()
                .AddScoped<IValidator<ListValueObject<UuidValueObject>>, ListValueObjectValidator<UuidValueObject>>()
                .AddScoped<IValidator<OrderByValueObject>, OrderByValueObjectValidator>()
                .AddScoped<IValidator<OrderTypeValueObject>, OrderTypeValueObjectValidator>()
                .AddScoped<IValidator<PageNumberRangeValueObject>, PageNumberRangeValueObjectValidator>()
                .AddScoped<IValidator<PageNumberValueObject>, PageNumberValueObjectValidator>()
                .AddScoped<IValidator<PageSizeValueObject>, PageSizeValueObjectValidator>()
                .AddScoped<IValidator<StringValueObject>, StringValueObjectValidator>()
                .AddScoped<IValidator<UuidValueObject>, UuidValueObjectValidator>();

            return services;
        }
    }
}
