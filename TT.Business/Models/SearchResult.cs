namespace TT.Business.Models;

public class SearchResult
{
    public ContextRequest ContextRequest { get; set; } = null!;
    public Clients.Base.Models.Route[] Routes { get; set; } = null!;
    public int ProviderId { get; set; }
}