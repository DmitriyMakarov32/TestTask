using Bogus;
using Microsoft.AspNetCore.Mvc;
using TT.Clients.ProviderTwo.Models;
using TT.Clients.ProviderTwo.Request;
using TT.Clients.ProviderTwo.Response;

namespace TT.ProviderTwo.Controllers;

[Route("api/v1/")]
public class ApiController : ControllerBase
{
    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> Search([FromBody]ProviderTwoSearchRequest request, CancellationToken cancellationToken)
    {
        var routeFake = new Faker<ProviderTwoRoute>()
            .RuleFor(x => x.Departure.Point, f => f.Address.City())
            .RuleFor(x => x.Arrival.Point, f => f.Address.City())
            .RuleFor(x => x.Departure.Date, f => f.Date.Between(DateTime.UtcNow, DateTime.Now.AddHours(3)))
            .RuleFor(x => x.Arrival.Date, f => f.Date.Between(DateTime.UtcNow.AddHours(4), DateTime.Now.AddHours(10)))
            .RuleFor(x => x.Price, f => f.Random.Decimal(1000m, 10000m))
            .RuleFor(x => x.TimeLimit, f => f.Date.Between(DateTime.UtcNow.AddHours(0), DateTime.Now.AddHours(1)));

        var routes = routeFake.Generate(20).ToArray();

        return Ok(new ProviderTwoSearchResponse
        {
            Routes = routes
        });
    }

    [HttpGet]
    [Route("ping")]
    public async Task<IActionResult> Ping(CancellationToken cancellationToken)
    {
        return Ok();
    }
}