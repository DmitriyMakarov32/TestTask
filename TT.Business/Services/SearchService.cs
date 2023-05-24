using Mapster;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TT.Business.Events;
using TT.Business.Interfaces;
using TT.Business.Models;
using TT.Data.DbContexts;
using TT.Data.Entities;
using TT.Shared.Extensions;
using SearchRequest = TT.Data.Models.Query.SearchRequest;

namespace TT.Business.Services;

public class SearchService : ISearchService
{
    private readonly IPublishEndpoint _publisher;
    private readonly TestTaskDbContext _dbContext;

    public SearchService(IPublishEndpoint publisher, TestTaskDbContext dbContext)
    {
        _publisher = publisher;
        _dbContext = dbContext;
    }

    public async Task<Guid> RunSearchAsync(SearchRequest request, CancellationToken cancellationToken)
    {
        var search = new Search(request);
        _dbContext.Searches.Add(search);
        await _dbContext.SaveChangesAsync(cancellationToken);
        await _publisher.Publish(new RunSearchEvent(search.Id), cancellationToken);
        return search.Id;
    }

    public async Task<SearchResponse> GetSearchAsync(Guid searchId, CancellationToken cancellationToken)
    {
        var search = await _dbContext.Searches
            .Include(x => x.SearchResults)
            .Include(x => x.Query)
            .FirstOrDefaultAsync(x => x.Id == searchId, cancellationToken);

        if (search is null)
        {
            throw new KeyNotFoundException($"Search with key {searchId} not found");
        }
        
        var routes = await _dbContext.Routes.Include(x => x.SearchResult)
            .WhereIf(search.Query.Data.Filters?.OnlyCached is null or false,
                x => search.SearchResults.Select(x => x.Id).Contains(x.SearchResultId))
            .WhereIf(search.Query.Data.Filters?.MaxPrice is not null,
                x => x.Price <= search.Query.Data.Filters!.MaxPrice)
            .WhereIf(search.Query.Data.Filters?.DestinationDateTime is not null,
                x => x.DestinationDateTime <= search.Query.Data.Filters!.DestinationDateTime)
            .WhereIf(search.Query.Data.Filters?.MinTimeLimit is not null,
                x => x.TimeLimit <= search.Query.Data.Filters!.MinTimeLimit)
            .Where(x => x.Origin == search.Query.Data.Origin)
            .Where(x => x.Destination == search.Query.Data.Destination)
            .Where(x => x.OriginDateTime >= search.Query.Data.OriginDateTime)
            .Where(x => x.TimeLimit > DateTime.UtcNow)
            .ToArrayAsync(cancellationToken);

        return (routes, search).Adapt<SearchResponse>();
    }
}