using Microsoft.AspNetCore.Mvc;
using TrafficLights.Api.Services;

namespace TrafficLights.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrafficControlController : ControllerBase
{
    private readonly ITrafficControlService _trafficControlService;

    public TrafficControlController(ITrafficControlService trafficControlService)
    {
        _trafficControlService = trafficControlService;
    }

    [HttpPost]
    [Route("start")]
    public async Task<IActionResult> PostTrafficStart()
    {
        await _trafficControlService.Start();
        return Ok();
    }

    [HttpPost]
    [Route("stop")]
    public async Task<IActionResult> PostTrafficStop()
    {
        await _trafficControlService.Stop();
        return Ok();
    }

    [HttpPost]
    [Route("standby")]
    public async Task<IActionResult> PostTrafficStandby()
    {
        await _trafficControlService.Standby();
        return Ok();
    }

    [HttpPost]
    [Route("shut")]
    public async Task<IActionResult> PostTrafficShut()
    {
        await _trafficControlService.Shut();
        return Ok();
    }

    [HttpPost]
    [Route("test")]
    public async Task<IActionResult> PostTrafficTest(int blinkTime, int pinNumber)
    {
        await _trafficControlService.Test(blinkTime, pinNumber);
        return Ok();
    }
}