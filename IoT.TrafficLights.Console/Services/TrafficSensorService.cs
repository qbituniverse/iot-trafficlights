using IoT.TrafficLights.Domain.Models.TrafficSensor;
using IoT.TrafficLights.Domain.Modules.TrafficSensor;
using Microsoft.Extensions.Logging;

namespace IoT.TrafficLights.Console.Services;

internal class TrafficSensorService : ITrafficSensorService
{
    private readonly ISensorModule _trafficSensorModule;
    private readonly ILogger<TrafficSensorService> _logger;

    public event EventHandler<TrafficSensor>? SensorValueChangedEvent;

    public TrafficSensorService(
        ISensorModule trafficSensorModule,
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