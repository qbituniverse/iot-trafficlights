namespace IoT.TrafficLights.Domain.Modules.TrafficControl;

public interface IPedestrianModule
{
    void Start();
    void Stop();
    void Shut();
}