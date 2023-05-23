using Bogus;
using Microsoft.AspNetCore.Mvc;
using TT.Clients.ProviderOne.Models;
using TT.Clients.ProviderOne.Request;
using TT.Clients.ProviderOne.Response;

namespace TT.ProviderOne.Controllers;

[Route("api/v1/")]
public class ApiController : ControllerBase
{
    [HttpPost]
    [Route("search")]
    public async Task<IActionResult> Search([FromBody]ProviderOneSearchRequest request, CancellationToken cancellationToken)
    {
        var routeFake = new Faker<ProviderOneRoute>()
            .RuleFor(x => x.From, f => f.Address.City())
            .RuleFor(x => x.To, f => f.Address.City())
            .RuleFor(x => x.DateFrom, f => f.Date.Between(DateTime.UtcNow, DateTime.Now.AddHours(3)))
            .RuleFor(x => x.DateTo,
                f => f.Date.Between(DateTime.UtcNow.AddHours(4), DateTime.Now.AddHours(10)))
            .RuleFor(x => x.Price, f => f.Random.Decimal(1000m, 10000m))
            .RuleFor(x => x.TimeLimit, f => f.Date.Between(DateTime.UtcNow.AddHours(0), DateTime.Now.AddHours(1)));

        var routes = routeFake.Generate(20).ToArray();

        return Ok(new ProviderOneSearchResponse
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