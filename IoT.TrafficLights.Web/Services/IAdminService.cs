namespace IoT.TrafficLights.Web.Services;

public interface IAdminService
{
    Task<bool> Ping();
    Task<string?> Config();
}