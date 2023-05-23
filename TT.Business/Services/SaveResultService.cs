using Mapster;
using TT.Business.Interfaces;
using TT.Data.DbContexts;
using TT.Data.Entities;
using TT.Data.Enum;
using SearchResult = TT.Business.Models.SearchResult;

namespace TT.Business.Services;

public class SaveResultService : ISaveResultService
{
    private readonly TestTaskDbContext _dbContext;

    public SaveResultService(TestTaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SearchResult> SaveResult(SearchResult searchResult)
    {
        _dbContext.SearchResults.Add(new Data.Entities.SearchResult()
        {
            Id = Guid.NewGuid(),
            Routes = searchResult.Routes.Adapt<Route[]>(),
            SearchId = searchResult.ContextRequest.SearchEvent.Id,
            SearchResultState = searchResult.Routes.Any() ?
                SearchResultStateEnum.Success :
                SearchResultStateEnum.Error,
            ProviderId = searchResult.ProviderId
        });
        await _dbContext.SaveChangesAsync(searchResult.ContextRequest.CancellationToken);
        return searchResult;
    }
}