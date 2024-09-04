using Microsoft.Extensions.Logging;
using TrafficLights.Console.Services;
using TrafficLights.Domain.Models.TrafficLog;

namespace TrafficLights.Console.Runs;

internal class RunTrafficTimer : IRun
{
    private readonly ITrafficControlService _trafficControlService;
    private readonly ILogger<RunTrafficTimer> _logger;

    public RunTrafficTimer(
        ITrafficControlService trafficControlService, 
        ILogger<RunTrafficTimer> logger)
    {
        _trafficControlService = trafficControlService;
        _logger = logger;
    }

    public async Task Run()
    {
        var trafficType = TrafficLog.TrafficMode.Start;
        var trafficChange = false;

        while (true)
        {
            if (trafficChange)
            {
                switch (trafficType)
                {
                    case TrafficLog.TrafficMode.Start:
                        trafficChange = false;
                        await _trafficControlService.Start();
                        break;
                    case TrafficLog.TrafficMode.Stop:
                        trafficChange = false;
                        await _trafficControlService.Stop();
                        break;
                    case TrafficLog.TrafficMode.Standby:
                        trafficChange = false;
                        await _trafficControlService.Standby();
                        break;
                }
            }
            else
            {
                // Run Standby during even minutes
                if (DateTime.Now.Minute % 2 == 0)
                {
                    if (trafficType == TrafficLog.TrafficMode.Standby)
                    {
                        trafficChange = true;
                        continue;
                    }

                    trafficChange = true;
                    trafficType = TrafficLog.TrafficMode.Standby;
                    _logger.LogInformation("Traffic Change to Standby");
                    continue;
                }

                // Run for switch over from Standby to Stop
                if (trafficType == TrafficLog.TrafficMode.Standby)
                {
                    trafficChange = true;
                    trafficType = TrafficLog.TrafficMode.Stop;
                    _logger.LogInformation("Traffic Change from Standby to Stop");
                    continue;
                }

                // Run Start-Stop during uneven minutes, toggle every 10 seconds
                if (DateTime.Now.Second % 10 == 0)
                {
                    trafficChange = true;
                    switch (trafficType)
                    {
                        case TrafficLog.TrafficMode.Start:
                            trafficType = TrafficLog.TrafficMode.Stop;
                            _logger.LogInformation("Traffic Change from Start to Stop");
                            break;
                        case TrafficLog.TrafficMode.Stop:
                            trafficType = TrafficLog.TrafficMode.Start;
                            _logger.LogInformation("Traffic Change from Stop to Start");
                            break;
                    }
                }
            }
        }
        // ReSharper disable once FunctionNeverReturns
    }
}