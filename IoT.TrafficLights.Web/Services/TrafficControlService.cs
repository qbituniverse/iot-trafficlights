using System.Net;

namespace IoT.TrafficLights.Web.Services;

public class TrafficControlService : ITrafficControlService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<TrafficControlService> _logger;

    public TrafficControlService(
        IHttpClientFactory httpClientFactory,
        ILogger<TrafficControlService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<bool> Start()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.PostAsync("api/trafficcontrol/start", null);
        _logger.LogInformation("Traffic Start");
        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> Stop()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.PostAsync("api/trafficcontrol/stop", null);
        _logger.LogInformation("Traffic Stop");
        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> Standby()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.PostAsync("api/trafficcontrol/standby", null);
        _logger.LogInformation("Traffic Standby");
        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<bool> Shut()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.PostAsync("api/trafficcontrol/shut", null);
        _logger.LogInformation("Traffic Shut");
        return response.StatusCode == HttpStatusCode.OK;
    }
}