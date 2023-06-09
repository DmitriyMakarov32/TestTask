using TT.Data.Enum;
using TT.Data.Models.Query;

namespace TT.Data.Entities;

public class Search
{
    public Search()
    {
        Id = Guid.NewGuid();
    }

    public Search(SearchRequest request)
    {
        Id = Guid.NewGuid();
        Query = new Query
        {
            Id = Guid.NewGuid(),
            Data = request
        };
        SearchState = SearchStateEnum.Created;
    }
    public Guid Id { get; set; }
    public SearchStateEnum SearchState { get; set; }
    public ICollection<SearchResult> SearchResults { get; set; } = null!;
    public Query Query { get; set; } = null!;
}