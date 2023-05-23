using JetBrains.Annotations;
using Mapster;
using TT.Clients.Base;
using TT.Clients.Base.Models;
using TT.Clients.Base.Models.Request;
using TT.Clients.ProviderOne.Request;
using TT.Clients.ProviderOne.Response;
using TT.Shared.Constants;

namespace TT.Clients.ProviderOne;

[UsedImplicitly]
public class ProviderOne : ProviderBase<ProviderOneSearchRequest, ProviderOneSearchResponse> ,ISearchClient
{

    public ProviderOne(IBaseApi<ProviderOneSearchRequest, ProviderOneSearchResponse> httpClient) : base(httpClient, Providers.ProviderOneId)
    {
    }
    public override async Task<(IReadOnlyCollection<Route> Routes, int ProviderId)> SearchAsync(ClientSearchRequest request, CancellationToken cancellationToken)
    {
        var responseFromClient = await BaseSearchAsync(HttpClient.SearchAsync, request.Adapt<ProviderOneSearchRequest>(), cancellationToken);

        if (responseFromClient != null)
            return (responseFromClient.Routes.Adapt<Route[]>(), ProviderId);

        return (Array.Empty<Route>(), ProviderId);
    }
}