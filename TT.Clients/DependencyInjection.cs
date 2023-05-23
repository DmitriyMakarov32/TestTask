using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using TT.Clients.Base;
using TT.Clients.Base.Settings;
using TT.Clients.ProviderOne.Request;
using TT.Clients.ProviderOne.Response;
using TT.Clients.ProviderTwo.Request;
using TT.Clients.ProviderTwo.Response;

namespace TT.Clients;

public static class DependencyInjection
{
    private const string DefaultSectionName = "ClientSettings";

    public static IServiceCollection AddClients(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName = DefaultSectionName)
    {
        var clientSettings = configuration
            .GetSection(sectionName)
            .Get<Dictionary<string, Settings>>();

        services.AddRefitClient<IBaseApi<ProviderOneSearchRequest, ProviderOneSearchResponse>>()
            .ConfigureHttpClient(c => c.BaseAddress =
                new Uri(clientSettings["ProviderOne"].Url));

        services.AddRefitClient<IBaseApi<ProviderTwoSearchRequest, ProviderTwoSearchResponse>>()
            .ConfigureHttpClient(c => c.BaseAddress =
                new Uri(clientSettings["ProviderTwo"].Url));

        services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ISearchClient)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}