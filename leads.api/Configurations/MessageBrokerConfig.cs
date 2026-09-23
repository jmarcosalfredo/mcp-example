using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using leads.api.Consumers;
using MassTransit;
using RabbitMQ.Client;

namespace leads.api.Configurations
{
    public static class MessageBrokerConfig
    {
        public static IServiceCollection AddMessageBrokerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration["RabbitMq:Host"];
            var username = configuration["RabbitMq:Username"];
            var password = configuration["RabbitMq:Password"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("MessageBroker configuration Error!");
            }

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<CreateLeadConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host, "/", h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
