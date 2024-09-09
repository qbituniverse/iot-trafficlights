using TrafficLights.Domain.Models.TrafficLog;
using TrafficLights.Domain.Modules.TrafficControl;
using TrafficLights.Domain.Repositories.TrafficLog;

namespace TrafficLights.Web.Services;

internal class TrafficControlService : ITrafficControlService
{
    private readonly ITrafficModule _trafficModule;
    private readonly IPedestrianModule _pedestrianModule;
    private readonly ITrafficLogRepository _trafficLogRepository;
    private readonly ILogger<TrafficControlService> _logger;

    public TrafficControlService(
        ITrafficModule trafficModule,
        IPedestrianModule pedestrianModule,
        ITrafficLogRepository trafficLogRepository,
        ILogger<TrafficControlService> logger)
    {
        _trafficModule = trafficModule;
        _pedestrianModule = pedestrianModule;
        _trafficLogRepository = trafficLogRepository;
        _logger = logger;
    }

    public async Task<bool> Start()
    {
        _pedestrianModule.Stop();
        _trafficModule.Start();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Start));
        _logger.LogInformation("Traffic Start");
        return true;
    }

    public async Task<bool> Stop()
    {
        _trafficModule.Stop();
        _pedestrianModule.Start();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Stop));
        _logger.LogInformation("Traffic Stop");
        return true;
    }

    public async Task<bool> Standby()
    {
        _pedestrianModule.Shut();
        _trafficModule.Shut();
        _trafficModule.Standby();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Standby));
        _logger.LogInformation("Traffic Standby");
        return true;
    }

    public async Task<bool> Shut()
    {
        _pedestrianModule.Shut();
        _trafficModule.Shut();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Shut));
        _logger.LogInformation("Traffic Shut");
        return true;
    }
}