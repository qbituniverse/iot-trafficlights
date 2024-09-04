using TrafficLights.Domain.Models.TrafficLog;

namespace TrafficLights.Api.Services;

public interface ITrafficLogService
{
    Task<TrafficLog> Read(string id);
    Task<IList<TrafficLog>> ReadByDate(DateOnly date);
    Task<IList<TrafficLog>> ReadAll();
    Task<string?> Create(TrafficLog trafficLog);
    Task<string?> Update(TrafficLog trafficLog);
    Task<int> Delete(string id);
    Task<int> DeleteByDate(DateOnly date);
    Task<int> DeleteAll();
}