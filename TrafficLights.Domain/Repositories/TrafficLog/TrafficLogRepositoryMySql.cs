using MySql.Data.MySqlClient;
using TrafficLights.Domain.Models.TrafficLog;

namespace TrafficLights.Domain.Repositories.TrafficLog;

public class TrafficLogRepositoryMySql : ITrafficLogRepository
{
    private string? ConnectionString { get; set; }

    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }

    public TrafficLogRepositoryMySql(string? connectionString)
    {
        ConnectionString = connectionString;

        using var connection = GetConnection();
        connection.Open();
        using var command = new MySqlCommand("SHOW TABLES FROM TrafficLights LIKE 'TrafficLogs';", connection);
        var reader = command.ExecuteScalar();

        if (reader != null) return;

        const string trafficLog = "CREATE TABLE TrafficLights.TrafficLogs (" +
                                  "Id INT NOT NULL AUTO_INCREMENT, " +
                                  "Mode VARCHAR(7) NOT NULL, " +
                                  "TimeStamp DATETIME NOT NULL, " +
                                  "PRIMARY KEY (Id));";
        command.CommandText = trafficLog;
        command.ExecuteNonQuery();
        connection.Close();
    }

    public async Task<Models.TrafficLog.TrafficLog> Read(string id)
    {
        if (!int.TryParse(id, out _)) return null!;

        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new MySqlCommand("SELECT * FROM TrafficLights.TrafficLogs " +
                                                   $"WHERE Id = '{id}';", connection);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var log = new TrafficLogMySql(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
            await connection.CloseAsync();
            return log.MapToTrafficLog();
        }

        return null!;
    }

    public async Task<IList<Models.TrafficLog.TrafficLog>> ReadByDate(DateOnly date)
    {
        if (!DateOnly.TryParse(date.ToString(), out _)) return null!;

        var fromDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        var toDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new MySqlCommand("SELECT * FROM TrafficLights.TrafficLogs " +
                                                   $"WHERE TimeStamp >= '{fromDate.ToDatabaseFormat()}' " +
                                                   $"AND TimeStamp <= '{toDate.ToDatabaseFormat()}' " +
                                                   "ORDER BY TimeStamp DESC;", connection);

        await using var reader = await command.ExecuteReaderAsync();
        var logs = new List<Models.TrafficLog.TrafficLog>();

        while (await reader.ReadAsync())
        {
            var trafficLogMySql = new TrafficLogMySql(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
            logs.Add(trafficLogMySql.MapToTrafficLog());
        }

        await connection.CloseAsync();

        return logs;
    }

    public async Task<IList<Models.TrafficLog.TrafficLog>> ReadAll()
    {
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new MySqlCommand("SELECT * FROM TrafficLights.TrafficLogs " +
                                                   "ORDER BY TimeStamp DESC;", connection);

        await using var reader = await command.ExecuteReaderAsync();
        var logs = new List<Models.TrafficLog.TrafficLog>();

        while (await reader.ReadAsync())
        {
            var trafficLogMySql = new TrafficLogMySql(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
            logs.Add(trafficLogMySql.MapToTrafficLog());
        }

        await connection.CloseAsync();

        return logs;
    }

    public async Task<string?> Create(Models.TrafficLog.TrafficLog trafficLog)
    {
        var trafficLogMySql = trafficLog.MapToMySql();
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new MySqlCommand("INSERT INTO TrafficLights.TrafficLogs (Mode, TimeStamp) " +
                                                   $"VALUES ('{trafficLogMySql.Mode}', " +
                                                   $"'{trafficLogMySql.TimeStamp.ToDatabaseFormat()}');" +
                                                   "SELECT LAST_INSERT_ID();", connection);

        var response = await command.ExecuteScalarAsync();
        await connection.CloseAsync();

        return response!.ToString();
    }

    public async Task<string?> Update(Models.TrafficLog.TrafficLog trafficLog)
    {
        if (!int.TryParse(trafficLog.Id, out _)) return null!;

        var trafficLogMySql = trafficLog.MapToMySql();
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new MySqlCommand("UPDATE TrafficLights.TrafficLogs " +
                                                   $"SET Mode = '{trafficLogMySql.Mode}', " +
                                                   $"TimeStamp = '{trafficLogMySql.TimeStamp.ToDatabaseFormat()}'" +
                                                   $"WHERE Id = {trafficLogMySql.Id};", connection);

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();

        return trafficLog.Id;
    }

    public async Task<int> Delete(string id)
    {
        if (!int.TryParse(id, out _)) return 0;

        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new MySqlCommand("DELETE FROM TrafficLights.TrafficLogs " +
                                                   $"WHERE Id = {id};", connection);

        var result = await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();

        return result;
    }

    public async Task<int> DeleteByDate(DateOnly date)
    {
        if (!DateOnly.TryParse(date.ToString(), out _)) return 0;

        var fromDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        var toDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new MySqlCommand("DELETE FROM TrafficLights.TrafficLogs " +
                                                   $"WHERE TimeStamp >= '{fromDate.ToDatabaseFormat()}' " +
                                                   $"AND TimeStamp <= '{toDate.ToDatabaseFormat()}' ", connection);

        var result = await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();

        return result;
    }

    public async Task<int> DeleteAll()
    {
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new MySqlCommand("DELETE FROM TrafficLights.TrafficLogs;", connection);

        var result = await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();

        return result;
    }
}