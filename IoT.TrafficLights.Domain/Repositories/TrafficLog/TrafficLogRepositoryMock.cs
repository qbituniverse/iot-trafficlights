namespace IoT.TrafficLights.Domain.Repositories.TrafficLog;

public class TrafficLogRepositoryMock : ITrafficLogRepository
{
    private static readonly Models.TrafficLog.TrafficLog TrafficLog1 = new("TL-123", Models.TrafficLog.TrafficLog.TrafficMode.Start);
    private static readonly Models.TrafficLog.TrafficLog TrafficLog2 = new("TL-456", Models.TrafficLog.TrafficLog.TrafficMode.Stop);
    private static readonly Models.TrafficLog.TrafficLog TrafficLog3 = new("TL-789", Models.TrafficLog.TrafficLog.TrafficMode.Standby);
    private static readonly IList<Models.TrafficLog.TrafficLog> TrafficLogs = new List<Models.TrafficLog.TrafficLog>
    {
        TrafficLog1, TrafficLog2, TrafficLog3
    };

    public async Task<Models.TrafficLog.TrafficLog> Read(string id)
    {
        return await Task.FromResult(TrafficLog1);
    }

    public async Task<IList<Models.TrafficLog.TrafficLog>> ReadByDate(DateOnly date)
    {
        return await Task.FromResult(TrafficLogs);
    }

    public async Task<IList<Models.TrafficLog.TrafficLog>> ReadAll()
    {
        return await Task.FromResult(TrafficLogs);
    }

    public async Task<string?> Create(Models.TrafficLog.TrafficLog trafficLog)
    {
        return await Task.FromResult(TrafficLog1.Id);
    }

    public async Task<string?> Update(Models.TrafficLog.TrafficLog trafficLog)
    {
        return await Task.FromResult(TrafficLog1.Id);
    }

    public async Task<int> Delete(string id)
    {
        return await Task.FromResult(2);
    }

    public async Task<int> DeleteByDate(DateOnly date)
    {
        return await Task.FromResult(2);
    }

    public async Task<int> DeleteAll()
    {
        return await Task.FromResult(3);
    }
}