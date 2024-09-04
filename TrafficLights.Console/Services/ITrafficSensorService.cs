using TrafficLights.Domain.Models.TrafficSensor;

namespace TrafficLights.Console.Services;

public interface ITrafficSensorService
{
    event EventHandler<TrafficSensor> SensorValueChangedEvent;
    void Invoke();
}