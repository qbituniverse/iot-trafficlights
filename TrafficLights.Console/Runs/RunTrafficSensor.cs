using Microsoft.Extensions.Logging;
using TrafficLights.Console.Services;
using TrafficLights.Domain.Models.TrafficLog;
using TrafficLights.Domain.Models.TrafficSensor;

namespace TrafficLights.Console.Runs;

internal class RunTrafficSensor : IRun
{
    private readonly ITrafficSensorService _trafficSensorService;
    private readonly ITrafficControlService _trafficControlService;
    private readonly ILogger<RunTrafficSensor> _logger;

    public bool IsMotionDetected { get; set; }

    public RunTrafficSensor(
        ITrafficSensorService trafficSensorService,
        ITrafficControlService trafficControlService,
        ILogger<RunTrafficSensor> logger)
    {
        _trafficSensorService = trafficSensorService;
        _trafficControlService = trafficControlService;
        _logger = logger;
    }

    public async Task Run()
    {
        _trafficSensorService.SensorValueChangedEvent += SensorValueChanged!;
        _trafficSensorService.Invoke();
        await _trafficControlService.Standby();
        var trafficType = TrafficLog.TrafficMode.Standby;

        await Task.Run(() =>
        {
            while (true)
            {
                if (IsMotionDetected && trafficType != TrafficLog.TrafficMode.Start)
                {
                    _logger.LogInformation("Traffic Change to Start");
                    _trafficControlService.Start();
                    trafficType = TrafficLog.TrafficMode.Start;
                    //Thread.Sleep(10000);
                }

                if (!IsMotionDetected && trafficType != TrafficLog.TrafficMode.Stop)
                {
                    _logger.LogInformation("Traffic Change to Stop");
                    _trafficControlService.Stop();
                    trafficType = TrafficLog.TrafficMode.Stop;
                }
            }
            // ReSharper disable once FunctionNeverReturns
        });
    }

    private void SensorValueChanged(object sender, TrafficSensor sensor)
    {
        IsMotionDetected = sensor.HighValue;
    }
}