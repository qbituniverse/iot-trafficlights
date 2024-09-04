namespace TrafficLights.Domain.Models.TrafficLog;

public class TrafficLogMySql
{
    public int? Id { get; set; }

    public string Mode { get; set; }

    public DateTime TimeStamp { get; set; }

    public TrafficLogMySql(int id, string mode, DateTime timeStamp)
    {
        Id = id;
        Mode = mode;
        TimeStamp = timeStamp;
    }
}