using Microsoft.AspNetCore.Mvc;
using TrafficLights.Api.Services;
using TrafficLights.Domain.Models.TrafficLog;

namespace TrafficLights.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrafficLogController : ControllerBase
{
    private readonly ITrafficLogService _trafficLogService;

    public TrafficLogController(ITrafficLogService trafficLogService)
    {
        _trafficLogService = trafficLogService;
    }

    [HttpGet]
    [Route("id/{id}")]
    public async Task<IActionResult> GetTrafficLogById(string id)
    {
        return Ok(await _trafficLogService.Read(id));
    }

    [HttpGet]
    [Route("date/{date}")]
    public async Task<IActionResult> GetTrafficLogByDate(string date)
    {
        return Ok(await _trafficLogService.ReadByDate(DateOnly.Parse(date)));
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllTrafficLogs()
    {
        return Ok(await _trafficLogService.ReadAll());
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrafficLog([FromBody] TrafficLog trafficLog)
    {
        return Ok(await _trafficLogService.Create(trafficLog));
    }

    [HttpPut]
    [Route("id/{id}")]
    public async Task<IActionResult> UpdateTrafficLogById(string id, [FromBody] TrafficLog trafficLog)
    {
        trafficLog.Id = id;
        return Ok(await _trafficLogService.Update(trafficLog));
    }

    [HttpDelete]
    [Route("id/{id}")]
    public async Task<IActionResult> DeleteTrafficLogById(string id)
    {
        return Ok(await _trafficLogService.Delete(id));
    }

    [HttpDelete]
    [Route("date/{date}")]
    public async Task<IActionResult> DeleteTrafficLogByDate(string date)
    {
        return Ok(await _trafficLogService.DeleteByDate(DateOnly.Parse(date)));
    }

    [HttpDelete]
    [Route("all")]
    public async Task<IActionResult> DeleteAllTrafficLogs()
    {
        return Ok(await _trafficLogService.DeleteAll());
    }
}