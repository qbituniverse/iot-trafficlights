namespace IoT.TrafficLights.Domain.Modules.TrafficControl;

public class PedestrianModuleMock : IPedestrianModule
{
    public void Start()
    {
        // Red On
        Thread.Sleep(500);
        // Red Off
        // Green On
    }

    public void Stop()
    {
        for (int i = 0; i < 10; i++)
        {
            // Green On
            Thread.Sleep(100);
            // Green Off
        }
        // Red On
    }

    public void Shut()
    {
        // All Off
    }
}