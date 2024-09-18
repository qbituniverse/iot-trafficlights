using System.Net;

namespace IoT.TrafficLights.Web.Services;

public class AdminService : IAdminService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<AdminService> _logger;

    public AdminService(
        IHttpClientFactory httpClientFactory,
        ILogger<AdminService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<bool> Ping()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.GetAsync("api/admin/ping");
        _logger.LogInformation("Admin Ping");
        return response.StatusCode == HttpStatusCode.OK;
    }

    public async Task<string?> Config()
    {
        var httpClient = _httpClientFactory.CreateClient("TrafficLightsApi");
        var response = await httpClient.GetAsync("api/admin/config");
        _logger.LogInformation("Admin Config");
        return await response.Content.ReadAsStringAsync();
    }
}