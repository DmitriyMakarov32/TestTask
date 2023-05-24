using TT.Clients.Base.Models;
using TT.Clients.Base.Models.Request;

namespace TT.Clients.Base;

public interface ISearchClient
{
    Task<(Route[] Routes, int ProviderId)> SearchAsync(ClientSearchRequest request, CancellationToken cancellationToken);
    Task<bool> IsAvailableAsync(CancellationToken cancellationToken);
}