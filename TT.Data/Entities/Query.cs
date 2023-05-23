using TT.Data.Models.Query;

namespace TT.Data.Entities;

public class Query
{
    public Guid Id { get; set; }
    public SearchRequest Data { get; set; }
    public Search Search { get; set; }

    public Guid SearchId { get; set; }
}