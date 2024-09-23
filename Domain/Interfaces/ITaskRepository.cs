public interface ITaskRepository
{
    Task<Task?> GetTaskByIdAsync(Guid id);
    Task<IEnumerable<Task>> GetAllTasksAsync();
    Task<bool> AddTaskAsync(Task task);
    Task<bool> UpdateTaskAsync(Task task);
    Task<bool> DeleteTaskAsync(Guid id);
}