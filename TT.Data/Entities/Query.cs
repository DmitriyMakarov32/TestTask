using TT.Data.Models.Query;

namespace TT.Data.Entities;

public class Query
{
    public Guid Id { get; set; }
    public SearchRequest Data { get; set; } = null!;
    public Search Search { get; set; } = null!;

    public Guid SearchId { get; set; }

}