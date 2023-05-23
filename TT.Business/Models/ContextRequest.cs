using TT.Business.Events;
using TT.Clients.Base;
using TT.Clients.Base.Models.Request;

namespace TT.Business.Models;

public class ContextRequest
{
    public RunSearchEvent SearchEvent { get; set; }
    public ClientSearchRequest Request { get; set; }
    public ISearchClient SearchClient { get; set; }
    public CancellationToken CancellationToken { get; set; }
}