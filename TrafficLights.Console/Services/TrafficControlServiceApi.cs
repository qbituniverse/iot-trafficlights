using Microsoft.Extensions.Logging;

namespace TrafficLights.Console.Services;

internal class TrafficControlServiceApi : ITrafficControlService
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

    public async Task Start()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        await httpClient.PostAsync("api/trafficcontrol/start", null);
        _logger.LogInformation("Traffic Start");
    }

    public async Task Stop()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        await httpClient.PostAsync("api/trafficcontrol/stop", null);
        _logger.LogInformation("Traffic Stop");
    }

    public async Task Standby()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        await httpClient.PostAsync("api/trafficcontrol/standby", null);
        _logger.LogInformation("Traffic Standby");
    }

    public async Task Shut()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        await httpClient.PostAsync("api/trafficcontrol/shut", null);
        _logger.LogInformation("Traffic Shut");
    }

    public async Task Test(int blinkTime, int pinNumber)
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        await httpClient.PostAsync($"api/trafficcontrol/test?blinkTime={blinkTime}&pinNumber={pinNumber}", null);
        _logger.LogInformation($"Traffic Test BlinkTime {blinkTime} PinNumber {pinNumber}");
    }
}