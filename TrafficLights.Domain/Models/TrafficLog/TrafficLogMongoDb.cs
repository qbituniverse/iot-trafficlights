using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TrafficLights.Domain.Models.TrafficLog;

public class TrafficLogMongoDb
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Mode { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime TimeStamp { get; set; }

    public TrafficLogMongoDb(string id, string mode, DateTime timeStamp)
    {
        Id = id;
        Mode = mode;
        TimeStamp = timeStamp;
    }
}