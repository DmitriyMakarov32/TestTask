using Refit;
using TT.Clients.Base.Models;
using TT.Clients.Base.Models.Request;

namespace TT.Clients.Base;

public abstract class ProviderBase<TReq, TRes> where TRes : class
{
    protected readonly IBaseApi<TReq, TRes> HttpClient;

    protected ProviderBase(IBaseApi<TReq, TRes> httpClient, int providerId)
    {
        ProviderId = providerId;
        HttpClient = httpClient;
    }

    public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
    {
        var responseFromClient = await HttpClient.PingAsync();
        return responseFromClient.IsSuccessStatusCode;
    }

    protected async Task<TRes?> BaseSearchAsync(Func<TReq, Task<ApiResponse<TRes>>> method,
        TReq request,
        CancellationToken cancellationToken)
    {
        var responseFromClient = await method(request);
        //TODO: Logs metrics
        return responseFromClient.IsSuccessStatusCode ? responseFromClient.Content : null;
    }


    public int ProviderId { get; }
    public abstract Task<(Route[] Routes, int ProviderId)> SearchAsync(ClientSearchRequest request, CancellationToken cancellationToken);
}