using System.Text.Json.Serialization;

namespace TrafficLights.Domain.Models.TrafficLog;

public class TrafficLog
{
    public string? Id { get; set; }

    public string Mode { get; set; }

    [JsonConverter(typeof(TrafficLogConverters.DateTimeJsonConverter))]
    public DateTime TimeStamp { get; set; }

    [JsonConstructor]
    public TrafficLog(string mode)
    {
        Mode = mode;
        TimeStamp = DateTime.Now;
    }

    public TrafficLog(TrafficMode mode)
    {
        Mode = mode.ToString();
        TimeStamp = DateTime.Now;
    }

    public TrafficLog(string? id, TrafficMode mode)
    {
        Id = id;
        Mode = mode.ToString();
        TimeStamp = DateTime.Now;
    }

    public TrafficLog(string? id, TrafficMode mode, DateTime timestamp)
    {
        Id = id;
        Mode = mode.ToString();
        TimeStamp = timestamp;
    }

    public enum TrafficMode
    {
        Start,
        Stop,
        Standby,
        Shut,
        Test
    }
}