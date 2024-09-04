using Microsoft.Extensions.Logging;
using TrafficLights.Domain.Models.TrafficSensor;
using TrafficLights.Domain.Modules.TrafficSensor;

namespace TrafficLights.Console.Services;

internal class TrafficSensorService : ITrafficSensorService
{
    private readonly ITrafficSensorModule _trafficSensorModule;
    private readonly ILogger<TrafficSensorService> _logger;

    public event EventHandler<TrafficSensor>? SensorValueChangedEvent;

    public TrafficSensorService(
        ITrafficSensorModule trafficSensorModule,
        ILogger<TrafficSensorService> logger)
    {
        _trafficSensorModule = trafficSensorModule;
        _logger = logger;
    }

    public void Invoke()
    {
        _trafficSensorModule.SensorValueChangedEvent += SensorValueChangedEvent;
        _trafficSensorModule.Invoke();
        _logger.LogInformation("Sensor Invoked");
    }
}