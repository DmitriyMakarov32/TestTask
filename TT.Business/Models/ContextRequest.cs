using TT.Business.Events;
using TT.Clients.Base;
using TT.Clients.Base.Models.Request;

namespace TT.Business.Models;

public class ContextRequest
{
    public ContextRequest(RunSearchEvent searchEvent,
        ISearchClient searchClient,
        ClientSearchRequest request,
        CancellationToken cancellationToken)
    {
        SearchEvent = searchEvent;
        SearchClient = searchClient;
        Request = request;
        CancellationToken = cancellationToken;
    }

    public RunSearchEvent SearchEvent { get; set; }
    public ClientSearchRequest Request { get; set; }
    public ISearchClient SearchClient { get; set; }
    public CancellationToken CancellationToken { get; set; }
}