using TrafficLights.Domain.Models.TrafficLog;
using TrafficLights.Domain.Modules.TrafficControl;
using TrafficLights.Domain.Repositories.TrafficLog;

namespace TrafficLights.Web.Services;

internal class TrafficControlService : ITrafficControlService
{
    private readonly ITrafficControlModule _trafficControlModule;
    private readonly ITrafficLogRepository _trafficLogRepository;
    private readonly ILogger<TrafficControlService> _logger;

    public TrafficControlService(
        ITrafficControlModule trafficControlModule,
        ITrafficLogRepository trafficLogRepository,
        ILogger<TrafficControlService> logger)
    {
        _trafficControlModule = trafficControlModule;
        _trafficLogRepository = trafficLogRepository;
        _logger = logger;
    }

    public async Task<bool> Start()
    {
        _trafficControlModule.Start();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Start));
        _logger.LogInformation("Traffic Start");
        return true;
    }

    public async Task<bool> Stop()
    {
        _trafficControlModule.Stop();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Stop));
        _logger.LogInformation("Traffic Stop");
        return true;
    }

    public async Task<bool> Standby()
    {
        _trafficControlModule.Shut();
        _trafficControlModule.Standby();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Standby));
        _logger.LogInformation("Traffic Standby");
        return true;
    }

    public async Task<bool> Shut()
    {
        _trafficControlModule.Shut();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Shut));
        _logger.LogInformation("Traffic Shut");
        return true;
    }

    public async Task<bool> Test(int blinkTime, int pinNumber)
    {
        _trafficControlModule.Shut();
        _trafficControlModule.Test(blinkTime, pinNumber);
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Test));
        _logger.LogInformation($"Traffic Test BlinkTime {blinkTime} PinNumber {pinNumber}");
        return true;
    }
}