using IoT.TrafficLights.Domain.Repositories.TrafficLog;
using IoT.TrafficLights.Web.Models;
using IoT.TrafficLights.Web.Services;

namespace IoT.TrafficLights.Web;

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

        services.AddHttpClient("TrafficLightsApi",
            httpClient => { httpClient.BaseAddress = new Uri(configuration.Api!.Url!); });

        services.AddSingleton<ITrafficControlService, TrafficControlService>();
        services.AddSingleton<ITrafficLogService, TrafficLogService>();
    }
}