using Mapster;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TT.Business.Events;
using TT.Business.Interfaces;
using TT.Business.Models;
using TT.Clients.Base;
using TT.Data.DbContexts;
using TT.Data.Entities;
using TT.Data.Enum;
using Route = TT.Business.Models.Route;
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
            .Include(x => x.Query)
            .Include(x => x.SearchResults)
            .ThenInclude(x => x.Routes)
            .FirstOrDefaultAsync(x => x.Id == searchId, cancellationToken);

        if (search is null)
        {
            throw new KeyNotFoundException($"Search with key {searchId} not found");
        }

        Data.Entities.Route[] routes;

        if (search.Query.Data.Filters?.OnlyCached is true)
        {
            routes = await _dbContext.Routes
                .Where(x => x.TimeLimit > DateTime.UtcNow)
                .ToArrayAsync(cancellationToken);
        }
        else
        {
            routes = search.SearchResults
                .Select(x => x.Routes)
                .SelectMany(x => x)
                .ToArray();
        }


        var result = new SearchResponse
        {
            Routes = routes.Adapt<Route[]>(),
            MaxPrice = routes.Max(x => x.Price),
            MinPrice = routes.Min(x => x.Price),
            SearchState = search.SearchState,
            MaxMinutesRoute = (int)routes.Max(x => x.DestinationDateTime - x.OriginDateTime).TotalMinutes,
            MinMinutesRoute = (int)routes.Min(x => x.DestinationDateTime - x.OriginDateTime).TotalMinutes
        };
        return result;
    }
}