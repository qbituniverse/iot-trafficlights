namespace IoT.TrafficLights.Domain.Models.TrafficLog;

public class TrafficLogSQLite
{
    public int? Id { get; set; }

    public string Mode { get; set; }

    public DateTime TimeStamp { get; set; }

    public TrafficLogSQLite(int id, string mode, DateTime timeStamp)
    {
        Id = id;
        Mode = mode;
        TimeStamp = timeStamp;
    }
}