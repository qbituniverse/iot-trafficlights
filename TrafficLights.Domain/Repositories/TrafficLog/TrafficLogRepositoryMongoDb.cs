using MongoDB.Driver;
using TrafficLights.Domain.Models.TrafficLog;

namespace TrafficLights.Domain.Repositories.TrafficLog;

public class TrafficLogRepositoryMongoDb : ITrafficLogRepository
{
    private readonly IMongoCollection<TrafficLogMongoDb> _collection;

    public TrafficLogRepositoryMongoDb(string? connectionString)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("TrafficLights");
        _collection = database.GetCollection<TrafficLogMongoDb>("TrafficLogs");
    }

    public async Task<Models.TrafficLog.TrafficLog> Read(string id)
    {
        var filter = Builders<TrafficLogMongoDb>.Filter.Eq(d => d.Id, id);
        var result = await _collection.Find(filter).FirstAsync();
        return result.MapToTrafficLog();
    }

    public async Task<IList<Models.TrafficLog.TrafficLog>> ReadByDate(DateOnly date)
    {
        var fromDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        var toDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        var filterBuilder = Builders<TrafficLogMongoDb>.Filter;
        var filter = filterBuilder.Gt(d => d.TimeStamp, fromDate) & filterBuilder.Lt(d => d.TimeStamp, toDate);
        var documents = await _collection.Find(filter).ToListAsync();
        var result = documents.OrderByDescending(d => d.TimeStamp).ToList();
        return result.Select(r => r.MapToTrafficLog()).ToList();
    }

    public async Task<IList<Models.TrafficLog.TrafficLog>> ReadAll()
    {
        var filter = Builders<TrafficLogMongoDb>.Filter.Empty;
        var documents = await _collection.Find(filter).ToListAsync();
        var result = documents.OrderByDescending(d => d.TimeStamp).ToList();
        return result.Select(r => r.MapToTrafficLog()).ToList();
    }

    public async Task<string?> Create(Models.TrafficLog.TrafficLog trafficLog)
    {
        await _collection.InsertOneAsync(trafficLog.MapToMongoDb());
        return trafficLog.Id;
    }

    public async Task<string?> Update(Models.TrafficLog.TrafficLog trafficLog)
    {
        var filter = Builders<TrafficLogMongoDb>.Filter.Eq(d => d.Id, trafficLog.Id);
        await _collection.ReplaceOneAsync(filter, trafficLog.MapToMongoDb());
        return trafficLog.Id;
    }

    public async Task<int> Delete(string id)
    {
        var filter = Builders<TrafficLogMongoDb>.Filter.Eq(d => d.Id, id);
        var response = await _collection.DeleteOneAsync(filter);
        return (int)response.DeletedCount;
    }

    public async Task<int> DeleteByDate(DateOnly date)
    {
        var fromDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        var toDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        var filterBuilder = Builders<TrafficLogMongoDb>.Filter;
        var filter = filterBuilder.Gt(d => d.TimeStamp, fromDate) & filterBuilder.Lt(d => d.TimeStamp, toDate);
        var response = await _collection.DeleteManyAsync(filter);
        return (int)response.DeletedCount;
    }

    public async Task<int> DeleteAll()
    {
        var filter = Builders<TrafficLogMongoDb>.Filter.Empty;
        var response = await _collection.DeleteManyAsync(filter);
        return (int)response.DeletedCount;
    }
}