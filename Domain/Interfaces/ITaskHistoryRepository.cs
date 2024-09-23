public interface ITaskHistoryRepository
{
    Task<TaskHistory?> GetHistoryByIdAsync(Guid id);
    Task<IEnumerable<TaskHistory>> GetAllHistoriesAsync();
    Task<bool> AddHistoryAsync(TaskHistory history);
    Task<bool> UpdateHistoryAsync(TaskHistory history);
    Task<bool> DeleteHistoryAsync(Guid id);
}