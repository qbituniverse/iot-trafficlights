using IoT.TrafficLights.Domain.Models.TrafficSensor;

namespace IoT.TrafficLights.Console.Services;

public interface ITrafficSensorService
{
    event EventHandler<TrafficSensor> SensorValueChangedEvent;
    void Invoke();
}