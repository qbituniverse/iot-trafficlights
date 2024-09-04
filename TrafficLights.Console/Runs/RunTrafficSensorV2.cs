using Iot.Device.Hcsr501;
using Microsoft.Extensions.Logging;
using TrafficLights.Console.Services;

namespace TrafficLights.Console.Runs;

internal class RunTrafficSensorV2 : IRun
{
    private readonly ITrafficControlService _trafficControlService;
    private readonly ILogger<RunTrafficSensorV2> _logger;

    public RunTrafficSensorV2(
        ITrafficControlService trafficControlService, 
        ILogger<RunTrafficSensorV2> logger)
    {
        _trafficControlService = trafficControlService;
        _logger = logger;
    }

    public Task Run()
    {
        var hcsr501 = new Hcsr501(21);
        _logger.LogInformation("Motion Start");
        _trafficControlService.Shut();

        while (true)
        {
            if (hcsr501.IsMotionDetected)
            {
                _logger.LogInformation("Motion Detected");
                _trafficControlService.Test(500, 17);
            }
            else
            {
                _logger.LogInformation("Motion NOT Detected");
                _trafficControlService.Test(500, 23);
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}