using Dapper;
using Npgsql;

public class TaskHistoryRepository : ITaskHistoryRepository
{
    public async Task<bool> AddHistoryAsync(TaskHistory history)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.AddHistory, history) > 0;
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteHistoryAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.DeleteHistory, new { Id = id }) > 0;
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<TaskHistory>> GetAllHistoriesAsync()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<TaskHistory>(SqlCommands.GetAllHistories);
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<TaskHistory>();
        }
    }

    public async Task<TaskHistory?> GetHistoryByIdAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<TaskHistory>(SqlCommands.GetHistoryById, new { Id = id });
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<bool> UpdateHistoryAsync(TaskHistory history)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.UpdateHistory, history) > 0;
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }
}


file class SqlCommands
{
    public const string ConnectionString = "Server=localhost;Port=5432;Database=task_management_db;Username=postgres;Password=12345";
    public const string AddHistory = "Insert into taskhistory(id, taskid, changedescription, changedat) Values(@id, @taskid, @changedescription, @changedat)";
    public const string UpdateHistory = "Update taskhistory set taskid=@taskid, changedescription=@changedescription, changedat=@changedat where id=@id";
    public const string DeleteHistory = "Delete from taskhistory where id=@id";
    public const string GetAllHistories = "Select * from taskhistory"; // Новый запрос
    public const string GetHistoryById = "Select * from taskhistory where id=@id";
}
