using System.Net;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TT.API.Models;
using TT.Business.Interfaces;

namespace TT.API;

[Route("api/v1/ping")]
public class PingController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;

    public PingController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet]
    public async Task<IActionResult> Health(CancellationToken cancellationToken)
    {
        HealthReport report = await _healthCheckService.CheckHealthAsync(cancellationToken);
        return report.Status == HealthStatus.Healthy ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
    }
}