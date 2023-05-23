namespace TT.Business.Consumers;
using MassTransit.ExtensionsDependencyInjectionIntegration;

public static class ConsumersExtension
{

    public static void AddConsumers(this IServiceCollectionBusConfigurator configurator)
    {
        configurator.AddConsumer<RunSearchConsumer>();
    }
}