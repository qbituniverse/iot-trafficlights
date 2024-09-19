using IoT.TrafficLights.Api.Models;
using IoT.TrafficLights.Api.Services;
using IoT.TrafficLights.Domain.Modules.TrafficControl;
using IoT.TrafficLights.Domain.Repositories.TrafficLog;

namespace IoT.TrafficLights.Api;

internal class Register
{
    public static void Services(ApiConfiguration configuration, IServiceCollection services)
    {
        services.AddControllers();
        services.AddHealthChecks();
        services.AddSwaggerGen();

        switch (configuration.Repository!.Type)
        {
            case "SQLite":
                services.AddSingleton<ITrafficLogRepository>(new TrafficLogRepositorySQLite(configuration.Repository.SQLite!.Url));
                break;

            case "MySql":
                services.AddSingleton<ITrafficLogRepository>(
                    new TrafficLogRepositoryMySql(configuration.Repository.MySql!.Url));
                break;

            case "MongoDb":
                services.AddSingleton<ITrafficLogRepository>(
                    new TrafficLogRepositoryMongoDb(configuration.Repository.MongoDb!.Url));
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
                break;

            default:
                services.AddSingleton<ITrafficModule, TrafficModuleMock>();
                services.AddSingleton<IPedestrianModule, PedestrianModuleMock>();
                break;
        }

        services.AddSingleton<ITrafficControlService, TrafficControlService>();
        services.AddSingleton<ITrafficLogService, TrafficLogService>();
    }
}