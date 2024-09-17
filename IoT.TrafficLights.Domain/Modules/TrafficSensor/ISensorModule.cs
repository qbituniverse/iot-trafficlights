namespace IoT.TrafficLights.Domain.Modules.TrafficSensor;

public interface ISensorModule
{
    event EventHandler<Models.TrafficSensor.TrafficSensor> SensorValueChangedEvent;
    void Invoke();
}