namespace TT.Data.Models.Query;

public class SearchRequest
{
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public DateTime OriginDateTime { get; set; }
    public SearchFilters? Filters { get; set; }
}