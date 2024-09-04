using System.Device.Gpio;
using Iot.Device.Hcsr501;
using Microsoft.Extensions.Logging;
using TrafficLights.Console.Services;

namespace TrafficLights.Console.Runs;

internal class RunTrafficSensorV1 : IRun
{
    private readonly ITrafficControlService _trafficControlService;
    private readonly ILogger<RunTrafficSensorV1> _logger;

    public RunTrafficSensorV1(
        ITrafficControlService trafficControlService, 
        ILogger<RunTrafficSensorV1> logger)
    {
        _trafficControlService = trafficControlService;
        _logger = logger;
    }

    public Task Run()
    {
        var hcsr501 = new Hcsr501(21);
        hcsr501.Hcsr501ValueChanged += SensorValueChanged;
        _logger.LogInformation("Motion Start");
        _trafficControlService.Shut();

        void SensorValueChanged(object sender, Hcsr501ValueChangedEventArgs args)
        {
            if (args.PinValue == PinValue.High)
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

        while (true)
        {
        }
        // ReSharper disable once FunctionNeverReturns
    }
}