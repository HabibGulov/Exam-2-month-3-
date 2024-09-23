using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CommentRepository : ICommentRepository
{
    public async Task<bool> AddCommentAsync(Comment comment)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.AddComment, comment) > 0;
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteCommentAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.DeleteComment, new { Id = id }) > 0;
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<Comment>(SqlCommands.GetComments);
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<Comment>();
        }
    }

    public async Task<Comment?> GetCommentByIdAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<Comment>(SqlCommands.GetCommentById, new { Id = id });
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<bool> UpdateCommentAsync(Comment comment)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.UpdateComment, comment) > 0;
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
    public const string AddComment = "Insert into comments(id, taskid, userid, content, createdat) Values(@id, @taskid, @userid, @content, @createdat)";
    public const string UpdateComment = "Update comments set taskid=@taskid, userid=@userid, content=@content, createdat=@createdat where id=@id";
    public const string DeleteComment = "Delete from comments where id=@id";
    public const string GetComments = "Select * from comments";
    public const string GetCommentById = "Select * from comments where id=@id";
}