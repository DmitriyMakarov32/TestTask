namespace TT.Business.Models;

public class SearchResult
{
    public ContextRequest ContextRequest { get; set; }
    public IReadOnlyCollection<Clients.Base.Models.Route> Routes { get; set; }
    public int ProviderId { get; set; }
}