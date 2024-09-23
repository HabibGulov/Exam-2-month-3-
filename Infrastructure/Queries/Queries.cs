using Dapper;
using Microsoft.VisualBasic;
using Npgsql;

public class Queries:IQueries
{
    public async Task<IEnumerable<UserWithTask>> GetUsersWithTasks()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<UserWithTask>(SqlCommands.GetUsersWithTasks);
            }

        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<UserWithTask>();
        }
    }
    public async Task<IEnumerable<CategoryWithTaskCount>> GetCategoriesWithTaskCount()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<CategoryWithTaskCount>(SqlCommands.GetCategoriesWithTaskCount);
            }

        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<CategoryWithTaskCount>();
        }
    }
    public async Task<IEnumerable<Category>> GetCategoriesByPriority(int priority)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<Category>(SqlCommands.GetCategoriesByPriority, new { Priority = priority });
            }

        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<Category>();
        }
    }
    public async Task<IEnumerable<TaskWithUserAndCategory>> GetTaskWithUserAndCategory(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<TaskWithUserAndCategory>(SqlCommands.GetCategoriesByPriority, new { Id = id });
            }

        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<TaskWithUserAndCategory>();
        }
    }
    public async Task<IEnumerable<Task>> GetOrderedTasks()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<Task>(SqlCommands.GetOrderedTasks);
            }

        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<Task>();
        }
    }
    public async Task<IEnumerable<TaskHistory>> GetTaskHistory(Guid taskid)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<TaskHistory>(SqlCommands.GetTaskHistory, new { TaskId = taskid });
            }

        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<TaskHistory>();
        }
    }
    public async Task<IEnumerable<Comment>> GetTaskCommentByUser(Guid taskid, Guid userid)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<Comment>(SqlCommands.GetTaskHistory, new { TaskId = taskid, UserId = userid });
            }

        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<Comment>();
        }
    }
    public async Task<IEnumerable<TaskAttachmentsAndUser>> GetTaskAttachmentAndUser(Guid taskid)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<TaskAttachmentsAndUser>(SqlCommands.GetTaskAttachmentsAndUser, new { TaskId = taskid });
            }

        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<TaskAttachmentsAndUser>();
        }
    }
    public async Task<IEnumerable<Task>> GetTasksByDuedate(DateTime dueDate)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<Task>(SqlCommands.GetTasksByDueDate, new { DueDate = dueDate });
            }

        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<Task>();
        }
    }
    public async Task<IEnumerable<Task>> GetTaskByPriorityAndStatus(int priority, bool isCompleted)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<Task>(SqlCommands.GetTasksByDueDate, new { Priority=priority, IsCompleted=isCompleted });
            }

        }
        catch (NpgsqlException ex)
        {
            System.Console.WriteLine(ex.Message);
            return new List<Task>();
        }
    }
}

file class SqlCommands
{
    public const string ConnectionString = @"Server=localhost;Port=5432;Database=task_management_db;Username=postgres;Password=12345";
    public const string GetUsersWithTasks = @"select u.id, u.username, u.email, u.passwordhash, u.createdat, t.id as taskid, t.title, t.description, t.iscompleted, t.priority, t.createdat as taskcreatedat
                                            from users u
                                            join tasks t on t.userid=u.id";
    public const string GetCategoriesWithTaskCount = @"select c.id, c.name, c.createdat, Count(t.id) as taskcount
                                                    from categories c
                                                    join tasks t on t.categoryid=c.id
                                                    group by c.id, c.name, c.createdat";
    public const string GetCategoriesByPriority = @"select * from tasks
                                                    where priority = @priority";
    public const string GetTaskWithUserAndCategory = @"select t.id, t.title, t.description, t.iscompleted, t.priority, t.createdat, u.id as userid, u.username, u.email, u.passwordhash, c.id as categoryid, c.name, c.createdat as categorycreatedat
                                                    from tasks t
                                                    join users u on u.id=t.userid
                                                    join categories c on c.id = t.categoryid
                                                    where u.id=@u.id";
    public const string GetOrderedTasks = @"select * from tasks
                                            order by duedate";
    public const string GetTaskHistory = @"select * from taskhistory
                                        where taskid=@taskid";
    public const string GetTaskCommentByUser = @"select c.Id, c.TaskId, c.UserId, c.Content, c.CreatedAt from comments c    
                                            where taskid=@taskid and userid=@userid";
    public const string GetTaskAttachmentsAndUser = @"select ta.id, ta.taskid, ta.filepath, ta.createdat, u.id as userid, u.username, u.email  from taskattachments ta
                                            join tasks t on t.id=ta.taskid
                                            join users u on u.id=t.userid
                                            where ta.taskid=@taskid";
    public const string GetTasksByDueDate = @"select * from tasks
                                            where duedate=@duedate";
    public const string GetTaskByPriorityAndStatus = @"select * from tasks
                                                    where priority=@priority and iscompleted=@iscompleted";
}