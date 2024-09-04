namespace TrafficLights.Console.Models;

public class ConsoleConfiguration
{
    public string? Environment { get; set; }

    public Repository? Repository { get; set; }

    public Modules? Modules { get; set; }

    public string? Run { get; set; }
}

public class Repository
{
    public string? Type { get; set; }

    public MySql? MySql { get; set; }

    public MongoDb? MongoDb { get; set; }
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

    public TrafficSensor? TrafficSensor { get; set; }
}

public class TrafficControl
{
    public string? Type { get; set; }

    public Api? Api { get; set; }
}

public class Api
{
    public string? Url { get; set; }
}

public class TrafficSensor
{
    public string? Type { get; set; }
}