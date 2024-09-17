namespace IoT.TrafficLights.Web.Models;

public class WebConfiguration
{
    public string? Environment { get; set; }

    public LogLevel? LogLevel { get; set; }

    public Repository? Repository { get; set; }

    public Api? Api { get; set; }
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

public class Api
{
    public string? Url { get; set; }
}