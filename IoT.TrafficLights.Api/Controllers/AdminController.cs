using System.Text.Json;
using IoT.TrafficLights.Api.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IoT.TrafficLights.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly ApiConfiguration _apiConfiguration;
    private readonly ILogger<AdminController> _logger;

    public AdminController(
        IOptions<ApiConfiguration> apiConfiguration, 
        ILogger<AdminController> logger)
    {
        _apiConfiguration = apiConfiguration.Value;
        _logger = logger;
    }

    [HttpGet]
    [Route("ping")]
    public IActionResult GetPing()
    {
        _logger.LogInformation("Ping");
        return Ok("Ping");
    }

    [HttpGet]
    [Route("config")]
    public IActionResult GetConfiguration()
    {
        _logger.LogInformation(JsonSerializer.Serialize(_apiConfiguration));
        return Ok(_apiConfiguration);
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError([FromServices] IHostEnvironment hostEnvironment)
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
        var problem = Problem(
            title: exception.Error.Message,
            instance: exception.Error.Source,
            detail: exception.Error.StackTrace);
        var problemDetails = (ProblemDetails) Problem().Value!;

        problemDetails.Extensions.TryGetValue("traceId", out var traceId);
        
        _logger.LogError(@"Error: TraceId {TraceId} Status {Status} Type {Type} Message {Message} Source {Source} StackTrace {StackTrace}",
            traceId, problemDetails.Status, problemDetails.Type,
            exception.Error.Message, exception.Error.Source, exception.Error.StackTrace);

        return !hostEnvironment.IsDevelopment() ? Problem() : problem;
    }
}