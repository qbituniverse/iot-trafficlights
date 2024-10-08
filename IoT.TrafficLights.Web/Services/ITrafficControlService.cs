﻿namespace IoT.TrafficLights.Web.Services;

public interface ITrafficControlService
{
    Task<bool> Start();
    Task<bool> Stop();
    Task<bool> Standby();
    Task<bool> Shut();
}