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

public class SendClientMethodsService : ISendClientMethodsService
{

    public async Task<SearchResult> SendClientsMethodRequest(ContextRequest contextRequest)
    {
        var result = await contextRequest.SearchClient.SearchAsync(contextRequest.Request, contextRequest.CancellationToken);

        return (contextRequest, result).Adapt<SearchResult>();
    }
}