using TT.Business.Models;

namespace TT.Business.Interfaces;

public interface ISendClientMethodsService
{
    Task<SearchResult> SendClientsMethodRequest(ContextRequest contextRequest);
}