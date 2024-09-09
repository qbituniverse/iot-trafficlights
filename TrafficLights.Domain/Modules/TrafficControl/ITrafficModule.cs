namespace TrafficLights.Domain.Modules.TrafficControl;

public interface ITrafficModule
{
    void Start();
    void Stop();
    void Standby();
    void Shut();
}