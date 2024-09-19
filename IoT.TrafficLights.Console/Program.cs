using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.Text.Json;
using IoT.TrafficLights.Console;
using IoT.TrafficLights.Console.Models;
using IoT.TrafficLights.Console.Runs;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings{(string.IsNullOrEmpty(environment) ? "" : "." + environment)}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
IConfiguration configuration = configBuilder.Build();

var consoleConfiguration = configuration.GetSection(nameof(ConsoleConfiguration)).Get<ConsoleConfiguration>()!;

var loggerConfiguration = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .Enrich.WithProcessName()
    .Enrich.WithEnvironmentName()
    .Enrich.WithEnvironmentUserName()
    .WriteTo.Console(
        restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(consoleConfiguration.LogLevel!.Console!),
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}",
        theme: AnsiConsoleTheme.Code);

switch (consoleConfiguration.Repository!.Type)
{
    case "SQLite":
        if (!File.Exists(consoleConfiguration.Repository!.SQLite!.Url))
            File.Create(consoleConfiguration.Repository!.SQLite!.Url!).Close();

        loggerConfiguration.WriteTo.SQLite(
            sqliteDbPath: consoleConfiguration.Repository.SQLite!.Url,
            tableName: "ConsoleLogs",
            restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(consoleConfiguration.LogLevel!.Database!));
        break;

    case "MySql":
        loggerConfiguration.WriteTo.MySQL(
            connectionString: consoleConfiguration.Repository.MySql!.Url,
            tableName: "ConsoleLogs",
            restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(consoleConfiguration.LogLevel!.Database!));
        break;

    case "MongoDb":
        loggerConfiguration.WriteTo.MongoDBBson(
            databaseUrl: $"{consoleConfiguration.Repository.MongoDb!.Url}/IoT-TrafficLights",
            collectionName: "ConsoleLogs",
            restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(consoleConfiguration.LogLevel!.Database!));
        break;
}

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        Register.Services(consoleConfiguration, services);
    })
    .ConfigureLogging(cfg =>
    {
        cfg.ClearProviders();
        cfg.AddSerilog(loggerConfiguration.CreateLogger());
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

AppDomain.CurrentDomain.UnhandledException += Error;

logger.LogInformation(@"IoT.TrafficLights.Console Configuration {Config}",
    JsonSerializer.Serialize(consoleConfiguration, new JsonSerializerOptions { WriteIndented = true }));

await Task.Run(host.Services.GetService<IRun>()!.Run);

void Error(object sender, UnhandledExceptionEventArgs e)
{
    var exception = e.ExceptionObject as Exception;

    Console.WriteLine(
        $"Error: Message {exception!.Message} Source {exception.Source} StackTrace {exception.StackTrace}");

    Thread.Sleep(15000);

    logger.LogError(@"Error: Message {Message} Source {Source} StackTrace {StackTrace}",
        exception.Message, exception.Source, exception.StackTrace);
    Thread.Sleep(15000);

    Environment.Exit(-1);
}
