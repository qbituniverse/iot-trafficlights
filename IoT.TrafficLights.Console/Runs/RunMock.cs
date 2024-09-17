using Microsoft.Extensions.Logging;

namespace IoT.TrafficLights.Console.Runs;

internal class RunMock : IRun
{
    private readonly ILogger<RunMock> _logger;

    public RunMock(ILogger<RunMock> logger)
    {
        _logger = logger;
    }

    public Task Run()
    {
        _logger.LogInformation("Mock Run Executed");

        Environment.Exit(-1);

        while (true)
        {
        }
        // ReSharper disable once FunctionNeverReturns
    }
}