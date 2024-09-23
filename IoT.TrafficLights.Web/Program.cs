using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.Text.Json;
using IoT.TrafficLights.Web;
using IoT.TrafficLights.Web.Models;

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
        restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(webConfiguration!.LogLevel!.Console!),
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}",
        theme: AnsiConsoleTheme.Code);

switch (webConfiguration.Repository!.Type)
{
    case "SQLite":
        if (!File.Exists(webConfiguration.Repository!.SQLite!.Url))
            File.Create(webConfiguration.Repository!.SQLite!.Url!).Close();

        loggerConfiguration.WriteTo.SQLite(
            sqliteDbPath: webConfiguration.Repository.SQLite!.Url,
            tableName: "WebLogs",
            restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(webConfiguration.LogLevel!.Database!));
        break;

    case "MySql":
        loggerConfiguration.WriteTo.MySQL(
            connectionString: webConfiguration.Repository.MySql!.Url,
            tableName: "WebLogs",
            restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(webConfiguration.LogLevel!.Database!));
        break;

    case "MongoDb":
        loggerConfiguration.WriteTo.MongoDBBson(
            databaseUrl: $"{webConfiguration.Repository.MongoDb!.Url}/IotTrafficLights",
            collectionName: "WebLogs",
            restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(webConfiguration.LogLevel!.Database!));
        break;
}

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerConfiguration.CreateLogger());
builder.WebHost.UseStaticWebAssets();

Register.Services(webConfiguration, builder.Services);

var app = builder.Build();

app.Logger.LogInformation(@"IoT.TrafficLights.Web Configuration {Config}",
    JsonSerializer.Serialize(webConfiguration, new JsonSerializerOptions { WriteIndented = true }));

app.UseExceptionHandler("/Error");

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");
app.MapHealthChecks("/healthz");

app.Run();