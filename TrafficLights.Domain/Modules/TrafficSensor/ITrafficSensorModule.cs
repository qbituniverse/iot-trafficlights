namespace TrafficLights.Domain.Modules.TrafficSensor;

public interface ITrafficSensorModule
{
    event EventHandler<Models.TrafficSensor.TrafficSensor> SensorValueChangedEvent;
    void Invoke();
}