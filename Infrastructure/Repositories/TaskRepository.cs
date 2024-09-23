using Dapper;
using Npgsql;

public class TaskRepository : ITaskRepository
{
    public async Task<bool> AddTaskAsync(Task task)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.AddTask, task) > 0;
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.DeleteTask, new { Id = id }) > 0;
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<Task>> GetAllTasksAsync()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<Task>(SqlCommands.GetTasks);
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<Task>();
        }
    }

    public async Task<Task?> GetTaskByIdAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<Task>(SqlCommands.GetTaskById, new { Id = id });
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<bool> UpdateTaskAsync(Task task)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.UpdateTask, task) > 0;
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
    public const string AddTask = "Insert into tasks(id, title, description, iscompleted, duedate, userid, categoryid, priority, createdat) Values(@id, @title, @description, @iscompleted, @duedate, @userid, @categoryid, @priority, @createdat)";
    public const string UpdateTask = "Update tasks set title=@title, description=@description, iscompleted=@iscompleted, duedate=@duedate, userid=@userid, categoryid=@categoryid, priority=@priority, createdat=@createdat where id=@id";
    public const string DeleteTask = "Delete from tasks where id=@id";
    public const string GetTasks = "Select * from tasks";
    public const string GetTaskById = "Select * from tasks where id=@id";
}