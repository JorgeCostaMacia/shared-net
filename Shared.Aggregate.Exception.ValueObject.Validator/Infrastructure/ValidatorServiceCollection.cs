using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Aggregate.Exception.ValueObject.Domain;
using Shared.Aggregate.Exception.ValueObject.Validator.Domain;
using Shared.ValueObject.Domain;
using Shared.ValueObject.Validator.Domain;

namespace Shared.Aggregate.Exception.ValueObject.Validator.Infrastructure
{
    public static class ValidatorServiceCollection
    {
        public static IServiceCollection AddExceptionValueObjectValidatorService(this IServiceCollection services)
        {
            services
                .AddScoped<IValidator<AggregateExceptionAggregateCode>, AggregateExceptionAggregateCodeValidator>()
                .AddScoped<IValidator<AggregateExceptionAggregateId>, AggregateExceptionAggregateIdValidator>()
                .AddScoped<IValidator<AggregateExceptionAggregateOccurredAt>, AggregateExceptionAggregateOccurredAtValidator>();

            return services;
        }
    }
}
