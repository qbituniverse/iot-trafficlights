using System.Data.SQLite;
using Serilog;
using MongoDB.Bson;
using TrafficLights.Web;
using TrafficLights.Web.Models;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<WebConfiguration>(
    builder.Configuration.GetSection(nameof(WebConfiguration))
);

var webConfiguration = builder.Configuration.GetSection(nameof(WebConfiguration)).Get<WebConfiguration>();

var loggerConfiguration = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .Enrich.WithProcessName()
    .Enrich.WithEnvironmentName()
    .Enrich.WithEnvironmentUserName()
    .WriteTo.Console(
        restrictedToMinimumLevel: LogEventLevel.Information,
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}",
        theme: AnsiConsoleTheme.Code);

switch (webConfiguration!.Repository!.Type)
{
    case "SQLite":
        if (!File.Exists(webConfiguration.Repository!.SQLite!.Url))
            SQLiteConnection.CreateFile(webConfiguration.Repository!.SQLite!.Url);

        loggerConfiguration.WriteTo.SQLite(
            sqliteDbPath: webConfiguration.Repository.SQLite!.Url,
            tableName: "WebLogs",
            restrictedToMinimumLevel: LogEventLevel.Warning);
        break;

    case "MySql":
        loggerConfiguration.WriteTo.MySQL(
            connectionString: webConfiguration.Repository.MySql!.Url,
            tableName: "WebLogs",
            restrictedToMinimumLevel: LogEventLevel.Warning);
        break;

    case "MongoDb":
        loggerConfiguration.WriteTo.MongoDBBson(
            databaseUrl: $"{webConfiguration.Repository.MongoDb!.Url}/TrafficLights",
            collectionName: "WebLogs",
            restrictedToMinimumLevel: LogEventLevel.Warning);
        break;
}

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerConfiguration.CreateLogger());
builder.WebHost.UseStaticWebAssets();

Register.Services(webConfiguration, builder.Services);

var app = builder.Build();

app.Logger.LogInformation(@"TrafficLights.Web Configuration {Config}", webConfiguration.ToJson());

app.UseExceptionHandler("/Error");

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");

app.Run();