using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Aggregate.Message.ValueObject.Domain;
using Shared.Aggregate.Message.ValueObject.Validator.Domain;

namespace Shared.Aggregate.Message.ValueObject.Validator.Infrastructure
{
    public static class ValidatorServiceCollection
    {
        public static IServiceCollection AddMessageValueObjectValidatorService(this IServiceCollection services)
        {
            services
                .AddScoped<IValidator<AggregateMessageAggregateId>, AggregateMessageAggregateIdValidator>()
                .AddScoped<IValidator<AggregateMessageAggregateOccurredAt>, AggregateMessageAggregateOccurredAtValidator>();

            return services;
        }
    }
}
