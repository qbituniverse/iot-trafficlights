using IoT.TrafficLights.Console.Models;
using IoT.TrafficLights.Console.Runs;
using IoT.TrafficLights.Console.Services;
using IoT.TrafficLights.Domain.Modules.TrafficControl;
using IoT.TrafficLights.Domain.Modules.TrafficSensor;
using IoT.TrafficLights.Domain.Repositories.TrafficLog;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.TrafficLights.Console;

internal class Register
{
    public static void Services(ConsoleConfiguration configuration, IServiceCollection services)
    {
        switch (configuration.Repository!.Type)
        {
            case "SQLite":
                services.AddSingleton<ITrafficLogRepository>(new TrafficLogRepositorySQLite(configuration.Repository.SQLite!.Url));
                break;

            case "MySql":
                services.AddSingleton<ITrafficLogRepository>(new TrafficLogRepositoryMySql(configuration.Repository.MySql!.Url));
                break;

            case "MongoDb":
                services.AddSingleton<ITrafficLogRepository>(new TrafficLogRepositoryMongoDb(configuration.Repository.MongoDb!.Url));
                break;

            default:
                services.AddSingleton<ITrafficLogRepository>(new TrafficLogRepositoryMock());
                break;
        }

        switch (configuration.Modules!.TrafficControl!.Type)
        {
            case "Pi":
                services.AddSingleton<ITrafficModule, TrafficModulePi>();
                services.AddSingleton<IPedestrianModule, PedestrianModulePi>();
                services.AddSingleton<ITrafficControlService, TrafficControlService>();
                break;

            default:
                services.AddSingleton<ITrafficModule, TrafficModuleMock>();
                services.AddSingleton<IPedestrianModule, PedestrianModuleMock>();
                services.AddSingleton<ITrafficControlService, TrafficControlService>();
                break;
        }

        switch (configuration.Modules!.TrafficSensor!.Type)
        {
            case "Pi":
                services.AddSingleton<ISensorModule, SensorModulePi>();
                services.AddSingleton<ITrafficSensorService, TrafficSensorService>();
                break;

            default:
                services.AddSingleton<ISensorModule, SensorModuleMock>();
                services.AddSingleton<ITrafficSensorService, TrafficSensorService>();
                break;
        }

        switch (configuration.Run)
        {
            case nameof(RunTrafficTimer):
                services.AddSingleton<IRun, RunTrafficTimer>();
                break;

            case nameof(RunTrafficSensor):
                services.AddSingleton<IRun, RunTrafficSensor>();
                break;

            default:
                services.AddSingleton<IRun, RunMock>();
                break;
        }
    }
}