using TT.Business.Events;
using TT.Business.Models;

namespace TT.Business.Interfaces;

public interface ISaveResultService
{
    Task<SearchResult> SaveResult(SearchResult searchResult);
}