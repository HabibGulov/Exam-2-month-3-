using Dapper;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region User
UserRepository userRepository = new UserRepository();
app.MapGet("api/Users", async () =>
{
    return await userRepository.GetAllUsersAsync();
});
app.MapGet("api/Users{id}", async (Guid id) =>
{
    return await userRepository.GetUserByIdAsync(id);
});
app.MapPost("api/Users", async (User user) =>
{
    return await userRepository.AddUserAsync(user);
});
app.MapPut("api/Users", async (User user) =>
{
    return await userRepository.UpdateUserAsync(user);
});
app.MapDelete("api/Users", async (Guid id) =>
{
    return await userRepository.DeleteUserAsync(id);
});
#endregion

#region Category
CategoryRepository categoryRepository = new CategoryRepository();
app.MapGet("api/Categories", async () =>
{
    return await categoryRepository.GetAllCategoriesAsync();
});
app.MapGet("api/Categories/{id}", async (Guid id) =>
{
    return await categoryRepository.GetCategoryByIdAsync(id);
});
app.MapPost("api/Categories", async (Category category) =>
{
    return await categoryRepository.AddCategoryAsync(category);
});
app.MapPut("api/Categories", async (Category category) =>
{
    return await categoryRepository.UpdateCategoryAsync(category);
});
app.MapDelete("api/Categories/{id}", async (Guid id) =>
{
    return await categoryRepository.DeleteCategoryAsync(id);
});
#endregion

#region Task
TaskRepository taskRepository = new TaskRepository();
app.MapGet("api/Tasks", async () =>
{
    return await taskRepository.GetAllTasksAsync();
});
app.MapGet("api/Tasks/{id}", async (Guid id) =>
{
    return await taskRepository.GetTaskByIdAsync(id);
});
app.MapPost("api/Tasks", async (Task task) =>
{
    return await taskRepository.AddTaskAsync(task);
});
app.MapPut("api/Tasks", async (Task task) =>
{
    return await taskRepository.UpdateTaskAsync(task);
});
app.MapDelete("api/Tasks/{id}", async (Guid id) =>
{
    return await taskRepository.DeleteTaskAsync(id);
});
#endregion

#region Comment
CommentRepository commentRepository = new CommentRepository();
app.MapGet("api/Comments", async () =>
{
    return await commentRepository.GetAllCommentsAsync();
});
app.MapGet("api/Comments/{id}", async (Guid id) =>
{
    return await commentRepository.GetCommentByIdAsync(id);
});
app.MapPost("api/Comments", async (Comment comment) =>
{
    return await commentRepository.AddCommentAsync(comment);
});
app.MapPut("api/Comments", async (Comment comment) =>
{
    return await commentRepository.UpdateCommentAsync(comment);
});
app.MapDelete("api/Comments/{id}", async (Guid id) =>
{
    return await commentRepository.DeleteCommentAsync(id);
});
#endregion

#region TaskAttachment
TaskAttachmentRepository taskAttachmentRepository = new TaskAttachmentRepository();
app.MapGet("api/Attachments", async () =>
{
    return await taskAttachmentRepository.GetAllAttachmentsAsync();
});
app.MapGet("api/Attachments/{id}", async (Guid id) =>
{
    return await taskAttachmentRepository.GetAttachmentByIdAsync(id);
});
app.MapPost("api/Attachments", async (TaskAttachment attachment) =>
{
    return await taskAttachmentRepository.AddAttachmentAsync(attachment);
});
app.MapPut("api/Attachments", async (TaskAttachment attachment) =>
{
    return await taskAttachmentRepository.UpdateAttachmentAsync(attachment);
});
app.MapDelete("api/Attachments/{id}", async (Guid id) =>
{
    return await taskAttachmentRepository.DeleteAttachmentAsync(id);
});
#endregion

#region TaskHistory
TaskHistoryRepository taskHistoryRepository = new TaskHistoryRepository();
app.MapGet("api/Histories", async () =>
{
    return await taskHistoryRepository.GetAllHistoriesAsync();
});
app.MapGet("api/Histories/{id}", async (Guid id) =>
{
    return await taskHistoryRepository.GetHistoryByIdAsync(id);
});
app.MapPost("api/Histories", async (TaskHistory history) =>
{
    return await taskHistoryRepository.AddHistoryAsync(history);
});
app.MapPut("api/Histories", async (TaskHistory history) =>
{
    return await taskHistoryRepository.UpdateHistoryAsync(history);
});
app.MapDelete("api/Histories/{id}", async (Guid id) =>
{
    return await taskHistoryRepository.DeleteHistoryAsync(id);
});
#endregion

#region Queries 

Queries queries = new Queries();
app.MapGet("api/GetUsersWithTasks", async ()=>
{
    return await queries.GetUsersWithTasks();
});
app.MapGet("api/GetCategoriesWithTaskCount", async ()=>
{
    return await queries.GetCategoriesWithTaskCount();
});
app.MapGet("api/GetCategoriesByPriority{priority}", async (int priority)=>
{
    return await queries.GetCategoriesByPriority  (priority);
});
app.MapGet("api/GetTasksWithUserAndCategory{id}", async (Guid id)=>
{
    return await queries.GetTaskWithUserAndCategory  (id);
});
app.MapGet("api/GetOrderedTasks", async ()=>
{
    return await queries.GetOrderedTasks() ;
});
app.MapGet("api/GetTaskHistory{taskid}", async (Guid taskid)=>
{
    return await queries.GetTaskHistory(taskid) ;
});
app.MapGet("api/GetTaskCommentByUser{taskid, userid}", async (Guid taskid, Guid userid)=>
{
    return await queries.GetTaskCommentByUser(taskid, userid) ;
});
app.MapGet("api/GetTaskAttachmentAndUser{taskid}", async (Guid taskid)=>
{
    return await queries.GetTaskAttachmentAndUser(taskid) ;
});
app.MapGet("api/GetTasksByDueDate{dueDate}", async (DateTime dueDate)=>
{
    return await queries.GetTasksByDuedate(dueDate) ;
});
app.MapGet("api/GetTasksByPriorityAndStatus{priority, status}", async (int priority, bool isCompleted)=>
{
    return await queries.GetTaskByPriorityAndStatus(priority, isCompleted) ;
});

#endregion



app.Run();
