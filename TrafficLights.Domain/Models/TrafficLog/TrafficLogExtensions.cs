using System.Globalization;

namespace TrafficLights.Domain.Models.TrafficLog;

public static class TrafficLogExtensions
{
    public static TrafficLogMySql MapToMySql(this TrafficLog trafficLog)
    {
        return new TrafficLogMySql(
            int.TryParse(trafficLog.Id, out _) ? int.Parse(trafficLog.Id) : 0, 
            trafficLog.Mode, 
            trafficLog.TimeStamp);
    }

    public static TrafficLog MapToTrafficLog(this TrafficLogMySql trafficLogMySql)
    {
        return new TrafficLog(
            trafficLogMySql.Id.ToString(),
            Enum.Parse<TrafficLog.TrafficMode>(trafficLogMySql.Mode),
            trafficLogMySql.TimeStamp);
    }

    public static TrafficLogMongoDb MapToMongoDb(this TrafficLog trafficLog)
    {
        return new TrafficLogMongoDb(trafficLog.Id!, trafficLog.Mode, trafficLog.TimeStamp);
    }

    public static TrafficLog MapToTrafficLog(this TrafficLogMongoDb trafficLogMongoDb)
    {
        return new TrafficLog(
            trafficLogMongoDb.Id,
            Enum.Parse<TrafficLog.TrafficMode>(trafficLogMongoDb.Mode),
            trafficLogMongoDb.TimeStamp);
    }

    public static string ToDatabaseFormat(this DateTime dateTime)
    {
        return $"{dateTime:yyyy-MM-dd HH:mm:ss}";
    }

    public static string ToModelFormat(this DateTime dateTime)
    {
        return $"{dateTime:dd-MM-yyyy HH:mm:ss}";
    }
}