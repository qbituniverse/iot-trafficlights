namespace TrafficLights.Domain.Repositories.TrafficLog;

public interface ITrafficLogRepository
{
    Task<Models.TrafficLog.TrafficLog> Read(string id);
    Task<IList<Models.TrafficLog.TrafficLog>> ReadByDate(DateOnly date);
    Task<IList<Models.TrafficLog.TrafficLog>> ReadAll();
    Task<string?> Create(Models.TrafficLog.TrafficLog trafficLog);
    Task<string?> Update(Models.TrafficLog.TrafficLog trafficLog);
    Task<int> Delete(string id);
    Task<int> DeleteByDate(DateOnly date);
    Task<int> DeleteAll();
}