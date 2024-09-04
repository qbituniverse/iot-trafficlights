using System.Net;

namespace TrafficLights.Web.Services;

public class TrafficControlServiceApi : ITrafficControlService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<TrafficControlServiceApi> _logger;

    public TrafficControlServiceApi(
        IHttpClientFactory httpClientFactory,
        ILogger<TrafficControlServiceApi> logger)
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

    public async Task<bool> Test(int blinkTime, int pinNumber)
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.PostAsync($"api/trafficcontrol/test?blinkTime={blinkTime}&pinNumber={pinNumber}", null);
        _logger.LogInformation($"Traffic Test BlinkTime {blinkTime} PinNumber {pinNumber}");
        return response.StatusCode == HttpStatusCode.OK;
    }
}