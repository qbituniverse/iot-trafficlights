using Microsoft.Extensions.Logging;
using TrafficLights.Domain.Models.TrafficLog;
using TrafficLights.Domain.Modules.TrafficControl;
using TrafficLights.Domain.Repositories.TrafficLog;

namespace TrafficLights.Console.Services;

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

    public async Task Start()
    {
        _pedestrianModule.Stop();
        _trafficModule.Start();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Start));
        _logger.LogInformation("Traffic Start");
    }

    public async Task Stop()
    {
        _trafficModule.Stop();
        _pedestrianModule.Start();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Stop));
        _logger.LogInformation("Traffic Stop");
    }

    public async Task Standby()
    {
        _pedestrianModule.Shut();
        _trafficModule.Shut();
        _trafficModule.Standby();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Standby));
        _logger.LogInformation("Traffic Standby");
    }

    public async Task Shut()
    {
        _pedestrianModule.Shut();
        _trafficModule.Shut();
        await _trafficLogRepository.Create(new TrafficLog(TrafficLog.TrafficMode.Shut));
        _logger.LogInformation("Traffic Shut");
    }
}