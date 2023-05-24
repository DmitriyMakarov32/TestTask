using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TT.Business.Interfaces;
using TT.Business.Services;
using TT.Business.Consumers;
using TT.Shared.Settings;

namespace TT.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddScoped<IGetClientMethodService, GetClientMethodsService>();
        services.AddScoped<ISendClientMethodsService, SendClientMethodsService>();
        services.AddScoped<ISaveResultService, SaveResultService>();
        services.AddScoped<ISearchService, SearchService>();

        var rabbitSettings = configuration
            .GetSection("RabbitSettings")
            .Get<RabbitSettings>();
        services.AddMassTransit(x =>
        {
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(rabbitSettings.Uri, rabbitSettings.Port,  rabbitSettings.VirtualHost, h => {
                    h.Username(rabbitSettings.Username);
                    h.Password(rabbitSettings.Password);
                });

                cfg.ConfigureEndpoints(provider);
            }));

            x.AddConsumers();
        });

        services.AddMassTransitHostedService();

        return services;
    }
}