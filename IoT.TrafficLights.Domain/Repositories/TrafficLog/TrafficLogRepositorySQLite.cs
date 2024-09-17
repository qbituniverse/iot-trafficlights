using IoT.TrafficLights.Domain.Models.TrafficLog;
using Microsoft.Data.Sqlite;

namespace IoT.TrafficLights.Domain.Repositories.TrafficLog;

public class TrafficLogRepositorySQLite : ITrafficLogRepository
{
    private string? ConnectionString { get; set; }

    private SqliteConnection GetConnection()
    {
        return new SqliteConnection($"Data Source={ConnectionString};");
    }

    public TrafficLogRepositorySQLite(string? connectionString)
    {
        ConnectionString = connectionString;

        using var connection = GetConnection();
        connection.Open();
        const string trafficLog = "CREATE TABLE IF NOT EXISTS TrafficLogs (" +
                                  "Id INTEGER NOT NULL, " +
                                  "Mode TEXT NOT NULL, " +
                                  "TimeStamp TEXT NOT NULL, " +
                                  "PRIMARY KEY (Id AUTOINCREMENT));";
        using var command = new SqliteCommand(trafficLog, connection);

        command.CommandText = trafficLog;
        command.ExecuteNonQuery();
        connection.Close();
    }

    public async Task<Models.TrafficLog.TrafficLog> Read(string id)
    {
        if (!int.TryParse(id, out _)) return null!;

        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new SqliteCommand("SELECT * FROM TrafficLogs " +
                                                    $"WHERE Id = {id};", connection);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var trafficLogSQLite = new TrafficLogSQLite(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
            await connection.CloseAsync();
            return trafficLogSQLite.MapToTrafficLog();
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
        await using var command = new SqliteCommand("SELECT * FROM TrafficLogs " +
                                                    $"WHERE TimeStamp >= '{fromDate.ToDatabaseFormat()}' " +
                                                    $"AND TimeStamp <= '{toDate.ToDatabaseFormat()}' " +
                                                    "ORDER BY TimeStamp DESC;", connection);

        await using var reader = await command.ExecuteReaderAsync();
        var logs = new List<Models.TrafficLog.TrafficLog>();

        while (await reader.ReadAsync())
        {
            var trafficLogSqLite = new TrafficLogSQLite(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
            logs.Add(trafficLogSqLite.MapToTrafficLog());
        }

        await connection.CloseAsync();

        return logs;
    }

    public async Task<IList<Models.TrafficLog.TrafficLog>> ReadAll()
    {
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new SqliteCommand("SELECT * FROM TrafficLogs " +
                                                    "ORDER BY TimeStamp DESC;", connection);

        await using var reader = await command.ExecuteReaderAsync();
        var logs = new List<Models.TrafficLog.TrafficLog>();

        while (await reader.ReadAsync())
        {
            var trafficLogSqLite = new TrafficLogSQLite(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2));
            logs.Add(trafficLogSqLite.MapToTrafficLog());
        }

        await connection.CloseAsync();

        return logs;
    }

    public async Task<string?> Create(Models.TrafficLog.TrafficLog trafficLog)
    {
        var trafficLogSQLite = trafficLog.MapToSQLite();
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new SqliteCommand("INSERT INTO TrafficLogs (Mode, TimeStamp) " +
                                                    $"VALUES ('{trafficLogSQLite.Mode}', " +
                                                    $"'{trafficLogSQLite.TimeStamp.ToDatabaseFormat()}');" +
                                                    "SELECT last_insert_rowid();", connection);

        var response = await command.ExecuteScalarAsync();
        await connection.CloseAsync();

        return response!.ToString();
    }

    public async Task<string?> Update(Models.TrafficLog.TrafficLog trafficLog)
    {
        if (!int.TryParse(trafficLog.Id, out _)) return null!;

        var trafficLogSQLite = trafficLog.MapToSQLite();
        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new SqliteCommand("UPDATE TrafficLogs " +
                                                    $"SET Mode = '{trafficLogSQLite.Mode}', " +
                                                    $"TimeStamp = '{trafficLogSQLite.TimeStamp.ToDatabaseFormat()}'" +
                                                    $"WHERE Id = {trafficLogSQLite.Id};", connection);

        await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();

        return trafficLog.Id;
    }

    public async Task<int> Delete(string id)
    {
        if (!int.TryParse(id, out _)) return 0;

        await using var connection = GetConnection();
        await connection.OpenAsync();
        await using var command = new SqliteCommand("DELETE FROM TrafficLogs " +
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
        await using var command = new SqliteCommand("DELETE FROM TrafficLogs " +
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
        await using var command = new SqliteCommand("DELETE FROM TrafficLogs;", connection);

        var result = await command.ExecuteNonQueryAsync();
        await connection.CloseAsync();

        return result;
    }
}