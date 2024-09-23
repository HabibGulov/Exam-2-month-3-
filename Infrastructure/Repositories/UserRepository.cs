using Dapper;
using Npgsql;

public class UserRepository : IUserRepository
{
    public async Task<bool> AddUserAsync(User user)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.AddUser, user) > 0;
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.DeleteUser, new { Id = id }) > 0;
            }
        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<User>(SqlCommands.GetUsers);
            }

        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<User>();
        }
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<User>(SqlCommands.GetUserById, new { Id = id });
            }
        }
        catch(NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new User();
        }
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommands.UpdateUser, user) > 0;
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
    public const string AddUser = "Insert into users(id, username, email, passwordhash, createdat)Values(@id, @username, @email, @passwordhash, @createdat)";
    public const string UpdateUser = "Update users set username=@username, email=@email, passwordhash=@passwordhash, createdat=@createdat where id=@id";
    public const string DeleteUser = "Delete from users where id=@id";
    public const string GetUsers = "Select * from users";
    public const string GetUserById = "Select * from users where id=@id";
}