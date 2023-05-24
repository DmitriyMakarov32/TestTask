namespace TT.Data.Entities;

public class Route
{
    public Guid Id { get; set; }
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public DateTime OriginDateTime { get; set; }
    public DateTime DestinationDateTime { get; set; }
    public decimal Price { get; set; }
    public DateTime TimeLimit { get; set; }
    public SearchResult SearchResult { get; set; } = null!;

    public Guid SearchResultId { get; set; }
}