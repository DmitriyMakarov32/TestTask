using TT.Business.Models;
using SearchRequest = TT.Data.Models.Query.SearchRequest;

namespace TT.Business.Interfaces;

public interface ISearchService
{
    Task<Guid> RunSearchAsync(SearchRequest request, CancellationToken cancellationToken);
    Task<SearchResponse> GetSearchAsync(Guid searchId, CancellationToken cancellationToken);
}