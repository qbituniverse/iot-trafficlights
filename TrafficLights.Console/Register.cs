using Microsoft.Extensions.DependencyInjection;
using TrafficLights.Console.Models;
using TrafficLights.Console.Runs;
using TrafficLights.Console.Services;
using TrafficLights.Domain.Modules.TrafficControl;
using TrafficLights.Domain.Modules.TrafficSensor;
using TrafficLights.Domain.Repositories.TrafficLog;

namespace TrafficLights.Console;

internal class Register
{
    public static void Services(ConsoleConfiguration configuration, IServiceCollection services)
    {
        switch (configuration.Repository!.Type)
        {
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
                services.AddSingleton<ITrafficControlModule, TrafficControlModulePi>();
                services.AddSingleton<ITrafficControlService, TrafficControlService>();
                break;

            case "Api":
                services.AddHttpClient("TrafficLightsApi",
                    httpClient => { httpClient.BaseAddress = new Uri(configuration.Modules!.TrafficControl.Api!.Url!); });
                services.AddSingleton<ITrafficControlService, TrafficControlServiceApi>();
                break;

            default:
                services.AddSingleton<ITrafficControlModule, TrafficControlModuleMock>();
                services.AddSingleton<ITrafficControlService, TrafficControlService>();
                break;
        }

        switch (configuration.Modules!.TrafficSensor!.Type)
        {
            case "Pi":
                services.AddSingleton<ITrafficSensorModule, TrafficSensorModulePi>();
                services.AddSingleton<ITrafficSensorService, TrafficSensorService>();
                break;

            default:
                services.AddSingleton<ITrafficSensorModule, TrafficSensorModuleMock>();
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

            case nameof(RunTrafficSensorV1):
                services.AddSingleton<IRun, RunTrafficSensorV1>();
                break;

            case nameof(RunTrafficSensorV2):
                services.AddSingleton<IRun, RunTrafficSensorV2>();
                break;

            default:
                services.AddSingleton<IRun, RunMock>();
                break;
        }
    }
}