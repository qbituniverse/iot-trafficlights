using TrafficLights.Domain.Models.TrafficLog;
using TrafficLights.Domain.Repositories.TrafficLog;

namespace TrafficLights.Api.Services;

public class TrafficLogService : ITrafficLogService
{
    private readonly ITrafficLogRepository _trafficLogRepository;
    private readonly ILogger<TrafficLogService> _logger;

    public TrafficLogService(
        ITrafficLogRepository trafficLogRepository,
        ILogger<TrafficLogService> logger)
    {
        _trafficLogRepository = trafficLogRepository;
        _logger = logger;
    }

    public async Task<TrafficLog> Read(string id)
    {
        _logger.LogInformation($"Traffic Log Get by Id {id}");
        return await _trafficLogRepository.Read(id);
    }

    public async Task<IList<TrafficLog>> ReadByDate(DateOnly date)
    {
        _logger.LogInformation($"Traffic Log Get by Date {date}");
        return await _trafficLogRepository.ReadByDate(date);
    }

    public async Task<IList<TrafficLog>> ReadAll()
    {
        _logger.LogInformation("Traffic Log Get All");
        return await _trafficLogRepository.ReadAll();
    }

    public async Task<string?> Create(TrafficLog trafficLog)
    {
        _logger.LogInformation($"Traffic Log Create {trafficLog.Id}");
        return await _trafficLogRepository.Create(trafficLog);
    }

    public async Task<string?> Update(TrafficLog trafficLog)
    {
        _logger.LogInformation($"Traffic Log Update {trafficLog.Id}");
        return await _trafficLogRepository.Update(trafficLog);
    }

    public async Task<int> Delete(string id)
    {
        _logger.LogInformation($"Traffic Log Delete by Id {id}");
        return await _trafficLogRepository.Delete(id);
    }

    public async Task<int> DeleteByDate(DateOnly date)
    {
        _logger.LogInformation($"Traffic Log Delete by Date {date}");
        return await _trafficLogRepository.DeleteByDate(date);
    }

    public async Task<int> DeleteAll()
    {
        _logger.LogInformation("Traffic Log Delete All");
        return await _trafficLogRepository.DeleteAll();
    }
}