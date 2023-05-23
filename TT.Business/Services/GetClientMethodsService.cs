using Mapster;
using Microsoft.EntityFrameworkCore;
using TT.Business.Events;
using TT.Business.Interfaces;
using TT.Business.Models;
using TT.Clients.Base;
using TT.Clients.Base.Models.Request;
using TT.Data.DbContexts;
using TT.Data.Enum;

namespace TT.Business.Services;

public class GetClientMethodsService : IGetClientMethodService
{
    private readonly TestTaskDbContext _dbContext;
    private readonly IEnumerable<ISearchClient> _searchClients;

    public GetClientMethodsService(TestTaskDbContext dbContext, IEnumerable<ISearchClient> searchClients)
    {
        _dbContext = dbContext;
        _searchClients = searchClients;
    }

    public async Task<IEnumerable<ContextRequest>> GetClientsMethods((RunSearchEvent searchEvent, CancellationToken cancellationToken) args)
    {
        var query = await _dbContext.Queries
            .Include(x => x.Search)
            .FirstOrDefaultAsync(x => x.Search.Id == args.searchEvent.Id);

        if (query == null)
            return Array.Empty<ContextRequest>();

        query.Search.SearchState = SearchStateEnum.Running;
        await _dbContext.SaveChangesAsync(args.cancellationToken);

        return _searchClients.Select(x => new ContextRequest
        {
            SearchEvent = args.searchEvent,
            SearchClient = x,
            Request = query.Adapt<ClientSearchRequest>(),
            CancellationToken = args.cancellationToken
        });

    }
}