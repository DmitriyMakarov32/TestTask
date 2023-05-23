using Mapster;
using Microsoft.AspNetCore.Mvc;
using TT.API.Models;
using TT.Business.Interfaces;

namespace TT.API;

[Route("api/v1/search")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpPost]
    public async Task<IActionResult> Search([FromBody]SearchRequest request, CancellationToken cancellationToken)
    {
        var searchId =
            await _searchService.RunSearchAsync(request.Adapt<Data.Models.Query.SearchRequest>(), cancellationToken);
        return Ok(searchId);
    }

    [HttpGet]
    public async Task<IActionResult> GetSearch([FromQuery]Guid searchId, CancellationToken cancellationToken)
    {
        var search =
            await _searchService.GetSearchAsync(searchId, cancellationToken);
        return Ok(search);
    }
}