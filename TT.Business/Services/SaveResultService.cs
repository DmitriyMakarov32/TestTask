using Mapster;
using TT.Business.Interfaces;
using TT.Data.DbContexts;
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
        _dbContext.SearchResults.Add(searchResult.Adapt<Data.Entities.SearchResult>());
        await _dbContext.SaveChangesAsync(searchResult.ContextRequest.CancellationToken);
        return searchResult;
    }
}