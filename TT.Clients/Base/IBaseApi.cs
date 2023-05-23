using Refit;

namespace TT.Clients.Base;

public interface IBaseApi<TReq, TRes>
{
    [Get("/api/v1/ping")]
    Task<ApiResponse<string>> PingAsync();

    [Post("/api/v1/search")]
    Task<ApiResponse<TRes>> SearchAsync([Body] TReq request);
}