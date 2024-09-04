using TrafficLights.Domain.Models.TrafficLog;
using TrafficLights.Domain.Repositories.TrafficLog;

namespace TrafficLights.Web.Services;

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

    public async Task<IList<TrafficLog>> GetAllTrafficLogs()
    {
        _logger.LogInformation("Traffic Log Get All");
        return await _trafficLogRepository.ReadAll();
    }

    public async Task<TrafficLog> GetTrafficLogById(string id)
    {
        _logger.LogInformation($"Traffic Log Get by Id {id}");
        return await _trafficLogRepository.Read(id);
    }
    public async Task<bool> CreateDocument(TrafficLog trafficLog)
    {
        _logger.LogInformation($"Traffic Log Create {trafficLog.Id}");
        var response = await _trafficLogRepository.Create(trafficLog);
        return response!.Length > 0;
    }

    public async Task<bool> UpdateDocument(TrafficLog trafficLog)
    {
        _logger.LogInformation($"Traffic Log Update {trafficLog.Id}");
        var response = await _trafficLogRepository.Update(trafficLog);
        return response!.Length > 0;
    }

    public async Task<bool> DeleteTrafficLogById(string id)
    {
        _logger.LogInformation($"Traffic Log Delete by Id {id}");
        var response = await _trafficLogRepository.Delete(id);
        return response > 0;
    }

    public async Task<bool> DeleteDocumentsByDate(string date)
    {
        _logger.LogInformation($"Traffic Log Delete by Date {date}");
        var response = await _trafficLogRepository.DeleteByDate(DateOnly.Parse(date));
        return response > 0;
    }

    public async Task<bool> DeleteAllTrafficLogs()
    {
        _logger.LogInformation("Traffic Log Delete All");
        var response = await _trafficLogRepository.DeleteAll();
        return response > 0;
    }
}