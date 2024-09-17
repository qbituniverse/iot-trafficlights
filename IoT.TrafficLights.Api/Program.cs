using System.Text.Json;
using IoT.TrafficLights.Api;
using IoT.TrafficLights.Api.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiConfiguration>(
    builder.Configuration.GetSection(nameof(ApiConfiguration))
);

var apiConfiguration = builder.Configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();

var loggerConfiguration = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithProcessId()
    .Enrich.WithProcessName()
    .Enrich.WithEnvironmentName()
    .Enrich.WithEnvironmentUserName()
    .WriteTo.Console(
        restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(apiConfiguration!.LogLevel!.Console!),
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}",
        theme: AnsiConsoleTheme.Code);

switch (apiConfiguration.Repository!.Type)
{
    case "SQLite":
        if (!File.Exists(apiConfiguration.Repository!.SQLite!.Url))
            File.Create(apiConfiguration.Repository!.SQLite!.Url!).Close();

        loggerConfiguration.WriteTo.SQLite(
            sqliteDbPath: apiConfiguration.Repository.SQLite!.Url,
            tableName: "ApiLogs",
            restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(apiConfiguration.LogLevel!.Database!));
        break;

    case "MySql":
        loggerConfiguration.WriteTo.MySQL(
            connectionString: apiConfiguration.Repository.MySql!.Url, 
            tableName: "ApiLogs",
            restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(apiConfiguration.LogLevel!.Database!));
        break;
        
    case "MongoDb":
        loggerConfiguration.WriteTo.MongoDBBson(
            databaseUrl: $"{apiConfiguration.Repository.MongoDb!.Url}/TrafficLights",
            collectionName: "ApiLogs",
            restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(apiConfiguration.LogLevel!.Database!));
        break;
}

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerConfiguration.CreateLogger());

Register.Services(apiConfiguration, builder.Services);

var app = builder.Build();

app.Logger.LogInformation(@"IoT.TrafficLights.Api Configuration {Config}",
    JsonSerializer.Serialize(apiConfiguration, new JsonSerializerOptions { WriteIndented = true }));

app.UseExceptionHandler("/error");

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();