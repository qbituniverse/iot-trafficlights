using TrafficLights.Api.Models;
using TrafficLights.Api.Services;
using TrafficLights.Domain.Modules.TrafficControl;
using TrafficLights.Domain.Repositories.TrafficLog;

namespace TrafficLights.Api;

internal class Register
{
    public static void Services(ApiConfiguration configuration, IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        switch (configuration.Repository!.Type)
        {
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
                services.AddSingleton<ITrafficControlModule, TrafficControlModulePi>();
                break;

            default:
                services.AddSingleton<ITrafficControlModule, TrafficControlModuleMock>();
                break;
        }

        services.AddSingleton<ITrafficControlService, TrafficControlService>();
        services.AddSingleton<ITrafficLogService, TrafficLogService>();
    }
}