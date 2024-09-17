using IoT.TrafficLights.Domain.Models.TrafficLog;

namespace IoT.TrafficLights.Web.Services;

public interface ITrafficLogService
{
    Task<IList<TrafficLog>> GetAllTrafficLogs();
    Task<TrafficLog> GetTrafficLogById(string id);
    Task<bool> CreateDocument(TrafficLog trafficLog);
    Task<bool> UpdateDocument(TrafficLog trafficLog);
    Task<bool> DeleteTrafficLogById(string id);
    Task<bool> DeleteDocumentsByDate(string date);
    Task<bool> DeleteAllTrafficLogs();
}