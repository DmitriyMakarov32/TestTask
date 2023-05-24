using TT.Data.Enum;

namespace TT.Data.Entities;

public class SearchResult
{
    public Guid Id { get; set; }
    public SearchResultStateEnum SearchResultState { get; set; }
    public int ProviderId { get; set; }
    public ICollection<Route> Routes { get; set; } = null!;
    public Search Search { get; set; } = null!;

    public Guid SearchId { get; set; }
}