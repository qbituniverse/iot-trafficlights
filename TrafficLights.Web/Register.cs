using TrafficLights.Domain.Modules.TrafficControl;
using TrafficLights.Domain.Repositories.TrafficLog;
using TrafficLights.Web.Models;
using TrafficLights.Web.Services;

namespace TrafficLights.Web;

internal class Register
{
    public static void Services(WebConfiguration configuration, IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();

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
                services.AddSingleton<ITrafficControlService, TrafficControlService>();
                break;

            case "Api":
                services.AddHttpClient("TrafficLightsApi",
                    httpClient => { httpClient.BaseAddress = new Uri(configuration.Modules!.TrafficControl.Api!.Url!); });
                services.AddSingleton<ITrafficControlService, TrafficControlServiceApi>();
                break;

            default:
                services.AddSingleton<ITrafficModule, TrafficModuleMock>();
                services.AddSingleton<IPedestrianModule, PedestrianModuleMock>();
                services.AddSingleton<ITrafficControlService, TrafficControlService>();
                break;
        }

        switch (configuration.TrafficLog!.Type)
        {
            case "Api":
                services.AddHttpClient("TrafficLightsApi",
                    httpClient => { httpClient.BaseAddress = new Uri(configuration.TrafficLog.Api!.Url!); });
                services.AddSingleton<ITrafficLogService, TrafficLogServiceApi>();
                break;

            default:
                services.AddSingleton<ITrafficLogService, TrafficLogService>();
                break;
        }
    }
}