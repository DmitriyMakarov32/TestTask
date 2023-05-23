using JetBrains.Annotations;
using Mapster;
using TT.Clients.Base;
using TT.Clients.Base.Models;
using TT.Clients.Base.Models.Request;
using TT.Clients.ProviderTwo.Request;
using TT.Clients.ProviderTwo.Response;
using TT.Shared.Constants;

namespace TT.Clients.ProviderTwo;

[UsedImplicitly]
public class ProviderTwo : ProviderBase<ProviderTwoSearchRequest, ProviderTwoSearchResponse> ,ISearchClient
{

    public ProviderTwo(IBaseApi<ProviderTwoSearchRequest, ProviderTwoSearchResponse> httpClient) : base(httpClient, Providers.ProviderTwoId)
    {
    }

    public override async Task<(IReadOnlyCollection<Route> Routes, int ProviderId)> SearchAsync(ClientSearchRequest request, CancellationToken cancellationToken)
    {
        var responseFromClient = await BaseSearchAsync(HttpClient.SearchAsync, request.Adapt<ProviderTwoSearchRequest>(), cancellationToken);

        if (responseFromClient != null)
            return (responseFromClient.Routes.Adapt<Route[]>(), ProviderId);

        return (Array.Empty<Route>(), ProviderId);
    }
}