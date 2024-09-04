namespace TrafficLights.Domain.Modules.TrafficControl;

public interface ITrafficControlModule
{
    void Start();
    void Stop();
    void Standby();
    void Shut();
    void Test(int blinkTime, int pinNumber);
}