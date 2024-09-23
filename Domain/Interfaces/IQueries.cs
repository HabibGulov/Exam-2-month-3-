public interface IQueries
{
    Task<IEnumerable<UserWithTask>> GetUsersWithTasks();
    Task<IEnumerable<CategoryWithTaskCount>> GetCategoriesWithTaskCount();
    Task<IEnumerable<Category>> GetCategoriesByPriority(int priority);
    Task<IEnumerable<TaskWithUserAndCategory>> GetTaskWithUserAndCategory(Guid id);
    Task<IEnumerable<Task>> GetOrderedTasks();
    Task<IEnumerable<TaskHistory>> GetTaskHistory(Guid taskid);
    Task<IEnumerable<Comment>> GetTaskCommentByUser(Guid taskid, Guid userid);
    Task<IEnumerable<TaskAttachmentsAndUser>> GetTaskAttachmentAndUser(Guid taskid);
    Task<IEnumerable<Task>> GetTasksByDuedate(DateTime dueDate);
    Task<IEnumerable<Task>> GetTaskByPriorityAndStatus(int priority, bool isCompleted);
}
