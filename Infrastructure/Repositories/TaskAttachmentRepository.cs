using Dapper;
using Npgsql;

public class TaskAttachmentRepository : ITaskAttachmentRepository
{
    public async Task<bool> AddAttachmentAsync(TaskAttachment attachment)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.AddAttachment, attachment) > 0;
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteAttachmentAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.DeleteAttachment, new { Id = id }) > 0;
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<TaskAttachment>> GetAllAttachmentsAsync()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<TaskAttachment>(SqlCommands.GetAllAttachments);
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<TaskAttachment>();
        }
    }

    public async Task<TaskAttachment?> GetAttachmentByIdAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<TaskAttachment>(SqlCommands.GetAttachmentById, new { Id = id });
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<bool> UpdateAttachmentAsync(TaskAttachment attachment)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.UpdateAttachment, attachment) > 0;
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
    public const string AddAttachment = "Insert into taskattachments(id, taskid, filepath, createdat) Values(@id, @taskid, @filepath, @createdat)";
    public const string UpdateAttachment = "Update taskattachments set taskid=@taskid, filepath=@filepath, createdat=@createdat where id=@id";
    public const string DeleteAttachment = "Delete from taskattachments where id=@id";
    public const string GetAllAttachments = "Select * from taskattachments"; // Новый запрос
    public const string GetAttachmentById = "Select * from taskattachments where id=@id";
}
