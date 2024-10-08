﻿namespace IoT.TrafficLights.Api.Models;

public class ApiConfiguration
{
    public string? Environment { get; set; }

    public LogLevel? LogLevel { get; set; }

    public Repository? Repository { get; set; }

    public Modules? Modules { get; set; }
}

public class LogLevel
{
    public string? Console { get; set; }

    public string? Database { get; set; }
}

public class Repository
{
    public string? Type { get; set; }

    public SQLite? SQLite { get; set; }

    public MySql? MySql { get; set; }

    public MongoDb? MongoDb { get; set; }
}

public class SQLite
{
    public string? Url { get; set; }
}

public class MySql
{
    public string? Url { get; set; }
}

public class MongoDb
{
    public string? Url { get; set; }
}

public class Modules
{
    public TrafficControl? TrafficControl { get; set; }
}

public class TrafficControl
{
    public string? Type { get; set; }
}