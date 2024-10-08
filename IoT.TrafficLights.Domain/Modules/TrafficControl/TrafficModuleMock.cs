﻿namespace IoT.TrafficLights.Domain.Modules.TrafficControl;

public class TrafficModuleMock : ITrafficModule
{
    public void Start()
    {
        // Red On
        Thread.Sleep(1000);
        // Amber On
        Thread.Sleep(2000);
        // Red/Amber Off
        // Green On
    }

    public void Stop()
    {
        // Green Off
        // Amber On
        Thread.Sleep(2000);
        // Amber Off
        // Red On
    }

    public void Standby()
    {
        Thread.Sleep(500);
        // Amber On
        Thread.Sleep(1000);
        // amber Off
        Thread.Sleep(500);
    }

    public void Shut()
    {
        // All Off
    }
}